function AGNet(onReceiveData) {
    this._onReceiveDataCallback = onReceiveData;

    this.getMap = function (pos) {

        this._onReceiveDataCallback();
    }
} 

function AGResLoader() {
    this.loadModel = function (modelId) {
        var model = new AGModel();

        // 获取测试模型
        var action = new AGAction();
        var direction = new AGDirection();

        if (modelId == 1) {
            direction.addFrame(new AGFrame(iFrame, '/res/5342', 80, 40, 0, 0));
            
        }
        else {
            for (var iFrame = 0; iFrame < 6; iFrame++) {
                direction.addFrame(new AGFrame(iFrame, '/data/models/1005/1005-0001-0001-000' + (iFrame + 1), 56, 89, 0, 0));
            }
        }
        action.addDirection(direction);
        model.addAction(action);
        return model;
    }
}