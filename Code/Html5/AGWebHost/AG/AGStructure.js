var MAPCELL_WIDTH = 64;
var MAPCELL_HEIGHT = 47;

function AGMap() {
    this._id = 0;
    this._caption = 'unknown';
    this._cells = new Array();
    this._range = null;
    this._row = 0;
    this._col = 0;

    this._curRow = 0;
    this._curCol = 0;

    this._objList = new Array();

    // 获取地图实际的宽度
    this.getActualWidth = function () {
        return (this._row + this._col) / 2 * MAPCELL_WIDTH;
    }

    // 获得地图实际的高度
    this.getActualHeight = function () {
        return (this._row + this._col) / 2 * MAPCELL_HEIGHT;
    }

    this.createObj = function (model, pos) {
        var obj = new AGObject(model, pos);
        this._objList.push(obj);
    }
}

function AGCamera(engine) {
    this._width = engine._settings.screen.w;
    this._height = engine._settings.screen.h;
    this._map = null;
    this._targetPt = new AGPt();
    this._zeroPt = new AGPt();
    this._isNeedUpdate = false;

    // 附加到地图上
    this.attach = function (map, targetRow, targetCol) {
        this._map = map;
        this.targetTo(targetRow, targetCol);
    }

    // 更改摄像机所观察的位置
    this.targetTo = function (targetRow, targetCol) {
        var cpt = this.convertCenterPt(new AGMapPos(targetRow, targetCol));
        this._targetPt._x = cpt._x;
        this._targetPt._y = cpt._y;
    }

    // 附加到地图的坐标点
    this.targetToPt = function (pt) {
        if (this._targetPt._x != pt._x
            || this._targetPt._y != pt._y) {
            this._targetPt._x = pt._x;
            this._targetPt._y = pt._y;
            this._isNeedUpdate = true;
        } else {
            this._isNeedUpdate = false;
        }
    }

    this.getZeroPt = function () {
        this._zeroPt._x = this._width / 2 - this._targetPt._x;
        this._zeroPt._y = (this._height / 2) - this._targetPt._y;
        return this._zeroPt;
    }

    this.getTargetPos = function () {
        return this.convertMapPtToPos(this._targetPt);
    }

    this.convertLTPt = function (pos) {
        var pt = new AGPt();

        pt._x = (pos._row + pos._col) * (MAPCELL_WIDTH / 2);
        pt._y = (pos._row - pos._col) * (MAPCELL_HEIGHT / 2) - (MAPCELL_HEIGHT / 2);
        return pt;
    }
    this.convertCenterPt = function (pos) {
        var pt = new AGPt();

        pt._x = (pos._row + pos._col) * (MAPCELL_WIDTH / 2) + (MAPCELL_WIDTH / 2);
        pt._y = (pos._row - pos._col) * (MAPCELL_HEIGHT / 2);
        return pt;
    }

    // 将窗口坐标转换为世界坐标
    this.convertWinPtToMapPt = function (winPt) {
        var zeroPt = this.getZeroPt();
        var mapX = winPt._x - zeroPt._x;
        var mapY = winPt._y - zeroPt._y;
        return new AGPt(mapX, mapY);
    }

    this.convertWinPtToPos = function (winPt) {
        var zeroPt = this.getZeroPt();
        var mapX = winPt._x - zeroPt._x;
        var mapY = winPt._y - zeroPt._y;

        return this.convertMapPtToPos(new AGPt(mapX, mapY));
    }

    this.convertMapPtToPos = function (mapPt) {
        var col = parseInt(mapPt._x / MAPCELL_WIDTH - mapPt._y / MAPCELL_HEIGHT);
        var row = parseInt(mapPt._x / MAPCELL_WIDTH + mapPt._y / MAPCELL_HEIGHT);
        return new AGMapPos(row, col);
    }
}

function AGMapCell() {
}

function AGMapPos(row, col) {
    this._row = row;
    this._col = col;
}

function AGPt(x, y) {
    this._x = x;
    this._y = y;
}

function AGModel(id) {
    this._id = id;
    this._actions = new Array();

    this.addAction = function (action) {
        var that = this;
        action._model = that;
        this._actions.push(action);
    }

    this.getFrame = function (actionId, directionId, frameId) {
        for (var actIndex = 0; actIndex < this._actions.length; actIndex++) {
            var action = this._actions[actIndex];
            if (action._id == actionId) {
                for (var dirIndex = 0; dirIndex < action._directions.length; dirIndex++) {
                    var direction = action._directions[dirIndex];
                    if (direction._id == directionId) {
                        for (var frameIndex = 0; frameIndex < direction._frames.length; frameIndex++) {
                            var frame = direction._frames[frameIndex];
                            if (frame._index == frameId) {
                                return frame;
                            }
                        }
                    }
                }
            }
        }
        return this._actions[0]._directions[0]._frames[0];
    }

    this.getFrameCount = function (actionId, directionId) {
        for (var actIndex = 0; actIndex < this._actions.length; actIndex++) {
            var action = this._actions[actIndex];
            if (action._id == actionId) {
                for (var dirIndex = 0; dirIndex < action._directions.length; dirIndex++) {
                    var direction = action._directions[dirIndex];
                    if (direction._id == directionId) {
                        return direction._frames.length;
                    }
                }
            }
        }
        return this._actions[0]._directions[0]._frames.length;
    }
}

//* --------------------------------------------------
// Model action structure
// -------------------------------------------------- */
function AGAction(id) {
    this._model = null;
    this._id = id;
    this._directions = new Array();

    this.addDirection = function (direction) {
        var that = this;
        direction._action = that;
        this._directions.push(direction);
    }
}

function AGDirection(id) {
    this._action = null;
    this._id = id;
    this._frames = new Array();

    this.addFrame = function (frame) {
        var that = this;
        frame._direction = that;
        this._frames.push(frame);
    }
}

function AGFrame(index, width, height, offsetX, offsetY) {
    this._direction = null;
    this._index = index;
    this._image = null;
    this._width = width;
    this._height = height;

    this._offsetX = offsetX;
    this._offsetY = offsetY;

    this.loadImage = function () {
        this._image = new Image();
        this._image.src = "Actions/GetFrameImage.ashx?m=" + this._direction._action._model._id + "&a=" + this._direction._action._id + "&d=" + this._direction._id + "&f=" + this._index;

    }
}

/// 这个要去掉
function AGObject(model, pos) {
    this._model = model;
    this._pos = pos;
}

function AGObj(model) {
    this._model = model;

    this._updateCounter = 5;
    this._updateTick = 1;

    this._actionId = 1;
    this._directionId = 1;
    this._frameIndex = 1;

    // 当前所在的地图格子
    this._sitePos = null;
    // 当前的地图坐标
    this._sitePt = null;

    // 目标坐标
    this._targetPt = null;

    this.setAction = function (action) {
        if (this._actionId != action) {
            this._actionId = action;
            this._frameIndex = 1;
        }
    }

    this.update = function () {
        this._updateTick++;
        if (this._updateTick < this._updateCounter) {
            return;
        }
        this._updateTick = 0;

        this._frameIndex++;
        var frameCount = this._model.getFrameCount(this._actionId, this._directionId);
        if (this._frameIndex > frameCount) {
            this._frameIndex = 1;
        }
        //alert(this._actionId +","+ this._directionId +"|"+this._frameIndex + "," + frameCount);

        if (this._targetPt != null && (this._targetPt._x != this._sitePt._x || this._targetPt._y != this._sitePt._x)) {

            var destAction = 1;
            if (this._targetPt._x > this._sitePt._x) {
                this._sitePt._x += 1;
                destAction = 2;
            } else if (this._targetPt._x < this._sitePt._x) {
                this._sitePt._x -= 1;
                destAction = 2;
            }

            if (this._targetPt._y > this._sitePt._y) {
                this._sitePt._y += 1;
                destAction = 2;
            } else if (this._targetPt._y < this._sitePt._y) {
                this._sitePt._y -= 1;
                destAction = 2;
            }

            this.setAction(destAction);
        }
    }
}