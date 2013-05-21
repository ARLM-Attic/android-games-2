function AGResLoader() {
    this.loadModel = function (modelId) {
        var model = new AGModel();
        var action = new AGAction();
        var direction = new AGDirection();

        direction.addFrame(new AGFrame(1, '1004-0001-0001-0001', 52, 91, 25, 80));
        direction.addFrame(new AGFrame(2, '1004-0001-0001-0002', 52, 91, 25, 80));
    }
}