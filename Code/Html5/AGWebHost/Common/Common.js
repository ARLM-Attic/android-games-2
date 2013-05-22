function strToJson(str) {
    if (str == "") {
        return null;
    }
    var json = eval('(' + str + ')');
    return json;
}

function isJson(obj) {
    var isjson = typeof (obj) == "object" && Object.prototype.toString.call(obj).toLowerCase() == "[object object]" && !obj.length;
    return isjson;
}