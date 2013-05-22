var Ajax = {
    method: 'post',
    asynchronous: true,
    proxy: null,
    contentType: "application/x-www-form-urlencoded",
    create: function () {
        var me = this, proxy;
        var xmlhttpObj = ['MSXML2.XMLHTTP.3.0',
                       'MSXML2.XMLHTTP',
                       'Microsoft.XMLHTTP'];
        if (window.XMLHttpRequest) {
            proxy = new XMLHttpRequest();
        }
        else if (typeof ActiveXObject != "undefined")// IE 
        {
            for (var i = 0, len = xmlhttpObj.length; i < len; i++) {
                if (!proxy) {
                    proxy = new ActiveXObject(xmlhttpObj[i]);
                }
                else {
                    break;
                }
            }
        }
        return proxy;
    },

    stateChange: function (proxy, callback) {
        var me = this;
        var readyState = proxy.readyState;
        if (readyState == 4 && proxy.status == 200) {
            (callback || function () { }).call(me, proxy.responseText, proxy);
        }
    },

    analyzeParam: function (params) {
        var arrp = [];
        for (var p in (params || {})) {
            arrp.push("&" + p + "=" + params[p]);
        }
        return arrp.join().replace(/\,/g, "");
    },

    request: function (cfg) {
        var me = this;
        var proxy = me.create();
        if (proxy) {
            proxy.onreadystatechange = function () {
                me.stateChange.call(me, proxy, cfg.callback);
            };
        }
        var param = me.analyzeParam(cfg.params);

        try {
            proxy.open(me.method, cfg.url, me.asynchronous);
            proxy.setRequestHeader("Content-type", me.contentType);
            proxy.send(me.analyzeParam.call(me, cfg.params));
        }
        catch (ex) {
            alert(ex.message)
        }
    }
};