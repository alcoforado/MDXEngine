var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
define(["require", "exports", '@angular/http', 'angular-in-memory-web-api', './app.component', './rxjs-extensions'], function (require, exports, http_1, angular_in_memory_web_api_1, app_component_1) {
    "use strict";
    var AppModule = (function () {
        function AppModule() {
        }
        AppModule = __decorate([
            NgModule({
                imports: [
                    BrowserModule,
                    FormsModule,
                    AppRoutingModule,
                    http_1.HttpModule,
                    angular_in_memory_web_api_1.InMemoryWebApiModule.forRoot(InMemoryDataService, { delay: 600 })
                ],
                declarations: [
                    app_component_1.AppComponent,
                    HeroSearchComponent,
                    routedComponents
                ],
                providers: [
                    HeroService
                ],
                bootstrap: [app_component_1.AppComponent]
            }), 
            __metadata('design:paramtypes', [])
        ], AppModule);
        return AppModule;
    }());
    exports.AppModule = AppModule;
});
