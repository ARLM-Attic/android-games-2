// user input device interface
function AGIDI(engine) {

    this._mouse = new MouseState();

    this.regEvent = function (engine) {
        var that = this;
        engine._canvas.addEventListener("mousedown", function (e) {
            that._mouse._x = e.layerX;
            that._mouse._y = e.layerY;
            that._mouse.setLBDown(1);
        });
        engine._canvas.addEventListener("mousemove", function (e) {
            that._mouse._x = e.layerX;
            that._mouse._y = e.layerY;
        });
        engine._canvas.addEventListener("mouseup", function (e) {
            that._mouse._x = e.layerX;
            that._mouse._y = e.layerY;
            that._mouse.setLBDown(0);
        });
    }

    this.regEvent(engine);
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

    this.setLBDown = function (state) {
        this._buttons[0] = state;
    }
    this.setRBDown = function (state) {
        this._buttons[1] = state;
    }

}