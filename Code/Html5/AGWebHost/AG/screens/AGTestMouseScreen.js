function AGTestMouseScreen() {
    this.init = function (engine) {
    }

    this.render = function (engine) {
        engine._gdi.drawString("x:" + engine._idi._mouse._x + ",y:" + engine._idi._mouse._y, 100, 0);
        if (engine._idi._mouse.isLBDown()) {
            engine._gdi.drawString('鼠标左键已经按下', 200, 0);
        } else {
            engine._gdi.drawString('鼠标左键已经弹开', 200, 0);
        }
    }

    this.loop = function (engine) {
    }
}