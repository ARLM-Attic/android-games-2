//* --------------------------------------------------
// engine
// -------------------------------------------------- */
function AGEngine() {
    this._settings = { screen: { w: 0, h: 0} };

    this._fps = 0;
    this._fpsCounter = 0;
    this._fpsTick = 0;
    this._lastTick = 0;
    this._intervalTick = 20;

    // graphics interface
    this._gdi = null;
    // user input interface
    this._idi = null;
    this._resLoader = null;
    this._net = null;

    this._items = new Array();

    this._curScreen = null;

    this._images = new Array();

    this.add = function (item) {
        this._items.push(item);
    }

    this.init = function (el, width, height) {
        var that = this;

        //this.loadData();

        this._canvas = el;
        this._context = this._canvas.getContext("2d");
        this._width = width;
        this._height = height;

        this._settings.screen.w = width;
        this._settings.screen.h = height;
        this._canvas.style.width = width;
        this._canvas.style.height = height;

        this._resLoader = new AGResLoader(this);
        this._net = new AGNet(function (cmd, data) { that.onReceiveNetData(cmd, data); });

        this._idi = new AGIDI(this);
        this._gdi = new AGGDI(this);
        setInterval(this.onTick, 10);
    }

    this.switchScreen = function (screen) {
        screen.init(this);
        this._curScreen = screen;
    }

    this.onRender = function () {
        var tick = Date.now();
        //alert(tick);
        if (tick - this._lastTick > this._intervalTick) {
            this._fpsCounter++;
            this._lastTick = tick;
            if (this._lastTick - this._fpsTick > 1000) {
                this._fps = this._fpsCounter;
                this._fpsCounter = 0;
                this._fpsTick = this._lastTick;
            }

            this._context.fillStyle = 'gray';
            this._context.fillRect(0, 0, this._width, this._height);

            if (this._curScreen != null) {
                this._curScreen.loop(this);
                this._curScreen.render(this);
            }

            this._gdi.drawString("fps:" + this._fps, 0, 0);
        }
    }

    this.onTick = function() {
        ag.engine.onRender();
    }

    this.loadData = function(){
        var image1 = new Image();
        image1.src = "Actions/getimage.ashx?file=/res/4588";
        this._images.push(image1);

        var image2 = new Image();
        image2.src = "Actions/getimage.ashx?file=/res/4589";
        this._images.push(image2);
    }

    this.onReceiveNetData = function (cmd, data) {
        if (this._curScreen != null) {
            this._curScreen.onReceiveNetData(cmd, data);
        }
    }
}

//* --------------------------------------------------
// graphics device interface
// -------------------------------------------------- */
function AGGDI(engine) {
    this._context = engine._context;

    this.draw = function (image, destX, destY, destW, destH, srcX, srcY, srcW, srcH) {
        if (arguments.length == 3) {
            this._context.drawImage(image, destX, destY);
        }
        else if (arguments.length == 5) {
            this._context.drawImage(image, destX, destY, destW, destH);
        } 
        else if (arguments.length == 9) {
            this._context.drawImage(image, srcX, srcY, srcW, srcH, destX, destY, destW, destH);
        }
    }

    this.drawString = function (text, x, y) {
        this._context.fillStyle = '#00f';
        //this._context.font = "italic 30px sans-serif";
        this._context.textBaseline = 'top'
        this._context.fillText(text, x, y);
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

//* --------------------------------------------------
// user input device interface
// -------------------------------------------------- */
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

//* --------------------------------------------------
// 鼠标信息定义
// -------------------------------------------------- */
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

    // 设置鼠标左键状态
    this.setLBDown = function (state) {
        this._buttons[0] = state;
    }

    // 设置鼠标右键状态
    this.setRBDown = function (state) {
        this._buttons[1] = state;
    }
}