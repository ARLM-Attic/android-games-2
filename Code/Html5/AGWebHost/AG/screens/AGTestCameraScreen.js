function AGTestCameraScreen() {
    this._model = null;
    this._model2 = null;
    this._camera = null;
    this._map = null;

    this.init = function (engine) {
        this._model = engine._resLoader.loadModel(1);
        this._model2 = engine._resLoader.loadModel(2);

        this._map = this.createMockMap();

        this._camera = new AGCamera(engine);
        this._camera.attach(this._map, 7, 7);

        this._map.createObj(this._model2, new AGMapPos(9, 9));
    }

    this.render = function (engine) {
        var zeroPt = this._camera.getZeroPt();
        var halfW = MAPCELL_WIDTH / 2;
        var halfH = MAPCELL_HEIGHT / 2;
        for (var r = 0; r < this._map._row; r++) {
            for (var c = 0; c < this._map._col; c++) {

                var x = (r + c) * halfW;
                var y = (r - c) * halfH - halfH;
                x = zeroPt._x + x;
                y = zeroPt._y + y;
                engine._gdi.draw2(this._model.getFrame(0, 0, 0)._image, x, y, MAPCELL_WIDTH, MAPCELL_HEIGHT); //, 0, 0, this._model.getFrame(0, 0, 0)._width, this._model.getFrame(0, 0, 0)._height);
                //engine._gdi.drawString("(" + r + "," + c + ")", x + 10, y + 10);
            }
        }

        for (var objIndex = 0; objIndex < this._map._objList.length; objIndex++) {
            var x = (this._map._objList[objIndex]._pos._row + this._map._objList[objIndex]._pos._col) * halfW;
            var y = (this._map._objList[objIndex]._pos._row - this._map._objList[objIndex]._pos._col) * halfH - halfH;
            x = zeroPt._x + x;
            y = zeroPt._y + y;
            engine._gdi.draw1(this._map._objList[objIndex]._model.getFrame(0, 0, 0)._image, x, y);
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
                //alert(pos._row + ',' + pos._col);
                this._camera.targetTo(pos._row, pos._col);
            }
        }
    }

    this.createMockMap = function () {
        var map = new AGMap();
        map._row = 15;
        map._col = 15;

        return map;
    }
}