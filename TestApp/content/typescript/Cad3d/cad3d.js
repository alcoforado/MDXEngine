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
var platform_browser_dynamic_1 = require("@angular/platform-browser-dynamic");
var platform_browser_1 = require("@angular/platform-browser");
var core_2 = require("@angular/core");
var angular_in_memory_web_api_1 = require("angular-in-memory-web-api");
var mocks_1 = require("../services/mocks");
var http_1 = require("@angular/http");
var shapes_mngr_service_1 = require("../services/shapes-mngr-service");
var shapes_mngr_component_1 = require("typescript/Cad3d/shapes-mngr.component");
var AppComponent = (function () {
    function AppComponent() {
    }
    return AppComponent;
}());
AppComponent = __decorate([
    core_2.Component({
        moduleId: module.id,
        selector: 'my-app',
        templateUrl: 'app.component.html'
    }),
    __metadata("design:paramtypes", [])
], AppComponent);
exports.AppComponent = AppComponent;
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [
            http_1.HttpModule,
            platform_browser_1.BrowserModule,
            angular_in_memory_web_api_1.InMemoryWebApiModule.forRoot(mocks_1.InMemMockService)
        ],
        declarations: [
            AppComponent,
            shapes_mngr_component_1.ShapesMngrComponent
        ],
        providers: [
            shapes_mngr_service_1.ShapesMngrService
        ],
        bootstrap: [AppComponent]
    }),
    __metadata("design:paramtypes", [])
], AppModule);
platform_browser_dynamic_1.platformBrowserDynamic().bootstrapModule(AppModule);
//# sourceMappingURL=cad3d.js.map