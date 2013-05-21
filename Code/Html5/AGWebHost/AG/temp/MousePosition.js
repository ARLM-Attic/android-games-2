var MouseInfo = function () {

    this._canvas = null;
    this._context = null;
    this._width = 0;
    this._height = 0;
    this._gdi = null;
    this._x = 0;
    this._y = 0;

    this.Init = function (canvas, height, width) {
        this._width = height;
        this._height = width;
        this._canvas = canvas;
        this._context = this._canvas.getContext("2d");
        this._canvas.setAttribute("width", this._width);
        this._canvas.setAttribute("height", this._height);
        this.RegEvent(this._canvas);
    }

    this.RegEvent = function (canvas) {
        var that = this;
        canvas.addEventListener("mousedown", function (e) {
            that._x = e.layerX;
            that._y = e.layerY;
        });
        canvas.addEventListener("mousemove", function (e) {
            that._x = e.layerX;
            that._y = e.layerY;
        });
        canvas.addEventListener("mouseup", function (e) {
            that._x = e.layerX;
            that._y = e.layerY;
        });
    }
}