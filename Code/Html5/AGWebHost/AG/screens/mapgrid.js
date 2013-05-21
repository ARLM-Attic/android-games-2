function AGMapGridScreen() {
    this.init = function (engine) {
    }

    this.render = function (engine) {
        for (var i = 0; i < 900; i++) {
            engine._gdi.draw(engine._images[0], i * 10, i * 10);
            engine._gdi.draw(engine._images[1], i * 20, i * 20);
        }
//        var halfW = 40;
//        var halfH = 20;
//        for (var r = 0; r < 2; r++) {
//            for (var c = 0; c < 1; c++) {

//                var x = (r + c) * 40;
//                var y = (r - c) * 20;
//                engine._gdi.drawDiamond(x, y, 80, 40);
//                engine._gdi.drawString("(" + r + "," + c + ")", x + 10, y + 10);
//            }
//        }
        // int col = (int)(ptInMap.X / MapCell.Width - ptInMap.Y / MapCell.Height);
        // int row = (int)(ptInMap.X / MapCell.Width + ptInMap.Y / MapCell.Height);
    }

    this.loop = function (engine) {
    }
}