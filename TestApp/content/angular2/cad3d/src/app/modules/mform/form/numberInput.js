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
var core_1 = require("@angular/core");
var shapes_mngr_service_1 = require("../services/shapes-mngr-service");
var ShapesMngrComponent = (function () {
    function ShapesMngrComponent(shapesMngrService) {
        this.shapesMngrService = shapesMngrService;
        this.shapeTypes = [];
        this.shapes = [];
    }
    ShapesMngrComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.shapesMngrService.GetTypesAsArray().subscribe(function (x) { return _this.shapeTypes = x; });
        this.shapesMngrService.GetShapes().subscribe(function (x) {
            return _this.shapes = x;
        });
    };
    return ShapesMngrComponent;
}());
ShapesMngrComponent = __decorate([
    core_1.Component({
        moduleId: module.id,
        selector: 'shapes-mngr',
        templateUrl: 'shapes-mngr.component.html',
        providers: [shapes_mngr_service_1.ShapesMngrService]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof shapes_mngr_service_1.ShapesMngrService !== "undefined" && shapes_mngr_service_1.ShapesMngrService) === "function" && _a || Object])
], ShapesMngrComponent);
exports.ShapesMngrComponent = ShapesMngrComponent;
var _a;
//# sourceMappingURL=numberInput.js.map