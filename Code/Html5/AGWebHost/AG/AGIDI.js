function AGIDI(engine) {

    this._mouse = null;

    this.update = function () {
        
    }
}

function MouseState() {
    this._x = 0;
    this._y = 0;
    this._buttons = [0, 0];

    // 判断鼠标左键是否按下
    this.isLBDown = function () {
        if (this._buttons[0] == 1) {
            return true;
        }
        return false;
    }

    // 判断鼠标右键是否按下
    this.isRBDown = function () {
        if (this._buttons[1] == 1) {
            return true;
        }
        return false;
    }
}