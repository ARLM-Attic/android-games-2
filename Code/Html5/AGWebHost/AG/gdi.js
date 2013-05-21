
function AGGDI(context) {
    this._context = context;
    this.draw = function (image, x, y, w, h) {
        //在左上角画一幅图片
        this._context.drawImage(image, x, y, w, h);
    }
    this.draw = function (image, x, y) {
        this._context.drawImage(image, x, y);

    }

    this.drawString = function (text, x, y) {
        this._context.fillStyle = '#00f';
        //this._context.font = "italic 30px sans-serif";
        this._context.textBaseline = 'top'
        this._context.fillText(text, x, y)
    }

    this.drawDiamond = function (x, y, w, h) {
        this._context.strokeStyle = '#00cc00';
        this._context.moveTo(x + w / 2, y);
        this._context.lineTo(x + w, y + h / 2);
        this._context.lineTo(x + w / 2, y + h);
        this._context.lineTo(x, y + h / 2);
        this._context.lineTo(x + w / 2, y);
        this._context.stroke();
    }
}