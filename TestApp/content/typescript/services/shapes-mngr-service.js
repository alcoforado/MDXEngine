"use strict";
var ShapeUI = (function () {
    function ShapeUI(typeName, shapeData, type) {
        this.typeName = typeName;
        this.shapeData = shapeData;
        this.type = type;
    }
    return ShapeUI;
}());
exports.ShapeUI = ShapeUI;
var ShapeType = (function () {
    function ShapeType(typeName, members) {
        this.typeName = typeName;
        this.members = members;
    }
    return ShapeType;
}());
exports.ShapeType = ShapeType;
var ShapeMember = (function () {
    function ShapeMember(fieldName, labelName, directiveType) {
        this.fieldName = fieldName;
        this.labelName = labelName;
        this.directiveType = directiveType;
    }
    return ShapeMember;
}());
exports.ShapeMember = ShapeMember;
var ShapeMngr = (function () {
    function ShapeMngr($http) {
        this.$http = $http;
        this.Types = null;
    }
    ShapeMngr.prototype.GetTypes = function () {
        var _this = this;
        if (this.Types == null) {
            this.Types = {};
            this.TypesArray = this.$wpf.postSync("shapesmngr/gettypes");
            this.TypesArray.forEach(function (elem) {
                _this.Types[elem.typeName] = elem;
            });
        }
        return this.Types;
    };
    ShapeMngr.prototype.GetTypesAsArray = function () {
        this.GetTypes();
        return this.TypesArray;
    };
    ShapeMngr.prototype.GetType = function (name) {
        if (typeof (this.GetTypes()[name]) == "undefined")
            throw "Type " + name + " not found";
        else {
            return this.Types[name];
        }
    };
    ShapeMngr.prototype.GetShapes = function () {
        var _this = this;
        var value = this.$wpf.postSync("shapesmngr/getshapes");
        value.forEach(function (elem) {
            elem.type = _this.GetType(elem.typeName);
        });
        return value;
    };
    ShapeMngr.prototype.CreateShape = function (type) {
        return this.$wpf.postSync("shapesmngr/createShape", {});
    };
    return ShapeMngr;
}());
exports.ShapeMngr = ShapeMngr;
//# sourceMappingURL=shapes-mngr-service.js.map