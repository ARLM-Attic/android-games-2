function AGMapGridScreen() {
    this.render = function (engine) {
        var halfW = 40;
        var halfH = 20;
        for (var r = 0; r < 20; r++) {
            for (var c = 0; c < 20; c++) {

                var x = (r + c) * 40;
                var y = (r - c) * 20;
                engine._gdi.drawDiamond(x, y, 80, 40);
                engine._gdi.drawString("(" + r + "," + c + ")", x + 10, y + 10);
            }
        }
        // int col = (int)(ptInMap.X / MapCell.Width - ptInMap.Y / MapCell.Height);
        // int row = (int)(ptInMap.X / MapCell.Width + ptInMap.Y / MapCell.Height);
    }
}