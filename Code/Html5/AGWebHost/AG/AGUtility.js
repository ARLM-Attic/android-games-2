﻿function AGNet(onReceiveData) {
    this._actionUrl = 'http://localhost:3003/AGI/Action.ashx?t=';
    this._onReceiveDataCallback = onReceiveData;

    // 获得可以看见的地图范围
    this.getMapRange = function (mapId, pos) {
        var that = this;
        Ajax.request({
            url: this._actionUrl + Date.now(),
            params: { cmd: 1002, map: mapId, cr: pos._row, cc: pos._col },
            callback: function (response) { that.onReceiveData(response); }
        });
    }

    // 获取玩家的位置信息
    this.getPlayerPos = function (mapId, player) {
        var that = this;
        Ajax.request({
            url: this._actionUrl + Date.now(),
            params: { cmd: 1000, map: mapId, player: player },
            callback: function (response) { that.onReceiveData(response); }
        });
    }

    // 获取模型信息
    this.getModel = function (modelId) {
        var that = this;
        Ajax.request({
            url: this._actionUrl + Date.now(),
            params: { cmd: 1003, model: modelId },
            callback: function (response) { that.onReceiveData(response); }
        });
    }

    this.onReceiveData = function (response) {
        //alert(response);
        var json = this.strToJson(response);
        //alert(json);
        this._onReceiveDataCallback(json.cmd, json.data);
    }

    this.strToJson = function(str) {
        if (str == "") {
            return null;
        }
        var json = eval('(' + str + ')');
        return json;
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
        else if (modelId == 4688) {
            direction.addFrame(new AGFrame(iFrame, '/data/models/4688/4688', 56, 89, 0, 0));
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

//* --------------------------------------------------
// A*
// -------------------------------------------------- */
function AGAStar() {

    this._openList = new Array();
    this._closeList = new Array();

    //
    // 获得路径
    // @cells
    // @startPos:AGMapPos
    //     开始位置
    // @targetPos:AGMapPos
    //     目标位置
    // @return:Array
    this.findPath = function (cells, startPos, targetPos) {

    }

    //从开启列表查找F值最小的节点
    this.getMinFFromOpenList = function () {
        var minPos = null;
        for (var index = 0; index < this._openList.length; index++) {
            var pos = this._openList[index];
            if (minPos == null || minPos._g + minPos._h > pos._g + pos._h) {
                minPos = pos;
            }
        }
        return minPos;
    }
}

function AGAStartPos(pos) {
    this._pos = pos;
    this._g = 0;
    this._h = 0;
    this._father = null;
}