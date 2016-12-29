"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var http_1 = require("@angular/http");
require("rxjs/Rx");
var mocks_1 = require("./mocks");
var core_1 = require("@angular/core");
var ShapeType = (function () {
    function ShapeType(typeName, members) {
        this.typeName = typeName;
        this.members = members;
    }
    return ShapeType;
}());
exports.ShapeType = ShapeType;
var ShapeUI = (function () {
    function ShapeUI(typeName, shapeData, type) {
        this.typeName = typeName;
        this.shapeData = shapeData;
        this.type = type;
    }
    return ShapeUI;
}());
exports.ShapeUI = ShapeUI;
var ShapeMember = (function () {
    function ShapeMember(fieldName, labelName, directiveType) {
        this.fieldName = fieldName;
        this.labelName = labelName;
        this.directiveType = directiveType;
    }
    return ShapeMember;
}());
exports.ShapeMember = ShapeMember;
var ShapesMngrService = (function () {
    function ShapesMngrService($http) {
        this.$http = $http;
        this.TypesHash = null;
        this.Types = null;
    }
    ShapesMngrService.prototype.extractData = function (res) {
        var body = res.json();
        return (body.data || {});
    };
    ShapesMngrService.prototype.GetTypes = function () {
        if (this.Types == null) {
            this.Types = this.$http.get("api/shapesmngr/types").map(this.extractData);
            this.TypesHash = this.Types.map(function (c) {
                var typeHash = {};
                c.forEach(function (elem) {
                    typeHash[elem.typeName] = elem;
                });
                return typeHash;
            });
        }
        return this.TypesHash;
    };
    ShapesMngrService.prototype.GetTypesAsArray = function () {
        this.GetTypes();
        return this.Types;
    };
    ShapesMngrService.prototype.GetType = function (name) {
        if (typeof (this.GetTypes()[name]) == "undefined")
            throw "Type " + name + " not found";
        else {
            return this.Types[name];
        }
    };
    ShapesMngrService.prototype.GetShapes = function () {
        var _this = this;
        return this.GetTypes().mergeMap(function (types) {
            return _this.$http.get('api/shapesmngr/shapes')
                .map(_this.extractData)
                .map(function (shapes) {
                shapes.forEach(function (elem) {
                    elem.type = types[elem.typeName];
                });
                return shapes;
            });
        });
    };
    return ShapesMngrService;
}());
ShapesMngrService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], ShapesMngrService);
exports.ShapesMngrService = ShapesMngrService;
mocks_1.InMemMockService.AddFixture('api_shapesmngr_types', [
    new ShapeType("OrthoMesh", [new ShapeMember("ElemsX", "Elems X", "int"), new ShapeMember("ElemsY", "Elems Y", "int")]),
    new ShapeType("OrthoMesh3D", [new ShapeMember("ElemsX", "Elems X", "int"), new ShapeMember("ElemsY", "Elems Y", "int"), new ShapeMember("ElemsZ", "Elems Z", "int")])
]);
mocks_1.InMemMockService.AddFixture('api_shapesmngr_shapes', [
    new ShapeUI("OrthoMesh", { ElemsX: 1, ElemsY: 2 }, null),
    new ShapeUI("OrthoMesh", { ElemsX: 3, ElemsY: 4 }, null),
    new ShapeUI("OrthoMesh", { ElemsX: 1, ElemsY: 5 }, null),
    new ShapeUI("OrthoMesh3D", { ElemsX: 1, ElemsY: 6, ElemsZ: 5 }, null)
]);
//# sourceMappingURL=shapes-mngr-service.js.map