function AGModel() {
}

function AGAction() {
}

function AGDirection() {
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