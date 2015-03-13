function AGTestModelScreen() {
    this._model = null;
    this.init = function (engine) {
        this._model = engine._resLoader.loadModel(1);
    }
        

    this.x = 10;
    this.y = 10;
    this.t = new Date().getSeconds();

    this.render = function (engine) {
        var _t = new Date().getSeconds();

        var deltaT = _t - this.t;
        if (deltaT > 0) {
            this.t = _t;

            this.x = this.x + 30 * deltaT;
        }

            engine._gdi.drawDiamond(this.x, this.y, 50, 50);
    }

    this.loop = function (engine) {
    }
}