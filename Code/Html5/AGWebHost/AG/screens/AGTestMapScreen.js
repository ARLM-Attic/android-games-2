function AGTestMapScreen() {
    this._engine = null;
    this._model = null;
    this._model2 = null;
    this._model3 = null;
    this._camera = null;
    this._map = null;

    this._playerPos = null;

    this._newRange = null;

    this.init = function (engine) {
        this._engine = engine;
        this._model = engine._resLoader.loadModel(1);
        this._model2 = engine._resLoader.loadModel(2);
        this._model3 = engine._resLoader.loadModel(4688);

        this._map = this.createMockMap();

        this._camera = new AGCamera(engine);
        engine._net.getPlayerPos(100, 'p1');

        this._map.createObj(this._model2, new AGMapPos(14, 9));
        this._map.createObj(this._model3, new AGMapPos(0, 13));

    }

    this.render = function (engine) {
        var zeroPt = this._camera.getZeroPt();
        var halfW = MAPCELL_WIDTH / 2;
        var halfH = MAPCELL_HEIGHT / 2;

        if (this._newRange != null) {
            this._map._range = this._newRange;
            this._newRange = null;
        }

        if (this._map._range != null) {
            for (var r = 0; r < this._map._range.r; r++) {
                for (var c = 0; c < this._map._range.c; c++) {
                    var row = this._map._range.sr + r;
                    var col = this._map._range.sc + c;

                    var x = (row + col) * halfW;
                    var y = (row - col) * halfH - halfH;
                    x = zeroPt._x + x;
                    y = zeroPt._y + y;

                    engine._gdi.draw(this._model.getFrame(0, 0, 0)._image, x, y, MAPCELL_WIDTH, MAPCELL_HEIGHT); //, 0, 0, this._model.getFrame(0, 0, 0)._width, this._model.getFrame(0, 0, 0)._height);
                    engine._gdi.drawString("(" + row + "," + col + ")", x + 20, y + 20);
                }
            }

            for (var objIndex = 0; objIndex < this._map._range.objs.length; objIndex++) {
                var x = this._map._range.objs[objIndex].px;
                var y = this._map._range.objs[objIndex].py;
                x = zeroPt._x + x;
                y = zeroPt._y + y;
                engine._gdi.draw(this._map._objList[objIndex]._model.getFrame(0, 0, 0)._image, x, y);
            }
        }

        engine._gdi.drawString("zeroPt:(" + zeroPt._x + "," + zeroPt._y + ")", 10, 500);
        engine._gdi.drawString("target pos:(" + this._camera._targetPos._row + "," + this._camera._targetPos._col + ")", 10, 520);

        var mapPt = this._camera.convertWinPtToMapPt(new AGPt(engine._idi._mouse._x, engine._idi._mouse._y));
        engine._gdi.drawString("mapPt:(" + mapPt._x + "," + mapPt._y + ")", 10, 540);

        var pos = this._camera.convertWinPtToPos(new AGPt(engine._idi._mouse._x, engine._idi._mouse._y));
        engine._gdi.drawString("pos:(" + pos._row + "," + pos._col + ")", 10, 560);
    }

    this._isMouseDown = false;
    this.loop = function (engine) {
        if (engine._idi._mouse.isLBDown()) {
            this._isMouseDown = true;
        }
        else {
            if (this._isMouseDown) {
                this._isMouseDown = false;
                var pos = this._camera.convertWinPtToPos(new AGPt(engine._idi._mouse._x, engine._idi._mouse._y));
                this._camera.targetTo(pos._row, pos._col);
                engine._net.getMapRange(100, pos);
            }
        }
    }

    this.createMockMap = function () {
        var map = new AGMap();
        map._row = 15;
        map._col = 20;

        return map;
    }

    this.onReceiveNetData = function (cmd, data) {
        if (cmd == 1002) {
            //alert(data.sr + "," + data.sc);
            this._newRange = data;

            //alert(data.data.cells);
        }
        else if (cmd == 1000) {
            this._playerPos = data;
            this._camera.attach(this._map, this._playerPos.pr, this._playerPos.pc);
            this._engine._net.getMapRange(100, new AGMapPos(this._playerPos.pr, this._playerPos.pc));
        }
    }
}