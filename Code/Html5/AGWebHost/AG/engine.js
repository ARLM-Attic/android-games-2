function AGEngine() {
    this._fps = 0;
    this._fpsCounter = 0;
    this._fpsTick = 0;
    this._lastTick = 0;
    this._intervalTick = 20;

    this._gdi = null;
    this._items = new Array();

    this._curScreen = null;

    this._images = new Array();

    this.add = function (item) {
        this._items.push(item);
    }

    this.init = function (el, width, height) {
        this.loadData();

        this._canvas = el;
        this._context = this._canvas.getContext("2d");
        this._width = width;
        this._height = height;
        this._gdi = new AGGDI(this._context);
        setInterval(this.onTick, 10);
    }

    this.switchScreen = function(screen){
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
                this._curScreen.render(this);

//                for (var i = 0; i < 900; i++) {
//                    this._gdi.draw(this._images[0], i * 10, i * 10);
//                    this._gdi.draw(this._images[1], i * 20, i * 20);
//                }
            }

            this._gdi.drawString("fps:" + this._fps, 0, 0);
        }
    }

    this.onTick = function() {
        ag.engine.onRender();
    }

    this.loadData = function(){
        var image1 = new Image();
        image1.src = "Actions/getimage.ashx?file=4588";
        this._images.push(image1);

        var image2 = new Image();
        image2.src = "Actions/getimage.ashx?file=4589";
        this._images.push(image2);
    }
}