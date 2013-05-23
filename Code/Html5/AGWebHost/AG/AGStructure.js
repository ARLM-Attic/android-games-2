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
    this._targetPos = new AGMapPos();

    this._zeroPt = new AGPt();

    // 附加到地图上
    this.attach = function (map, targetRow, targetCol) {
        this._map = map;
        this._targetPos._row = targetRow;
        this._targetPos._col = targetCol;
    }

    // 更改摄像机所观察的位置
    this.targetTo = function (targetRow, targetCol) {
        this._targetPos._row = targetRow;
        this._targetPos._col = targetCol;
    }

    this.getZeroPt = function () {
        var centerPt = this.convertCenterPt(this._targetPos);
        this._zeroPt._x = this._width / 2 - centerPt._x;
        this._zeroPt._y = (this._height / 2) - centerPt._y;
        return this._zeroPt;
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

function AGModel() {
    this._actions = new Array();

    this.addAction = function (action) {
        this._actions.push(action);
    }

    this.getFrame = function (actionId, directionId, frameId) {
        return this._actions[actionId]._directions[directionId]._frames[frameId];
    }
}

function AGAction() {
    this._directions = new Array();

    this.addDirection = function (direction) {
        this._directions.push(direction);
    }
}

function AGDirection() {
    this._frames = new Array();

    this.addFrame = function (frame) {
        this._frames.push(frame);
    }
}

function AGFrame(id, src, width, height, offsetX, offsetY) {
    this._id = id;
    this._image = new Image();
    this._image.src = "Actions/getimage.ashx?file=" + src;

    this._width = width;
    this._height = height;

    this._offsetX = offsetX;
    this._offsetY = offsetY;
}

function AGObject(model, pos) {
    this._model = model;
    this._pos = pos;
}