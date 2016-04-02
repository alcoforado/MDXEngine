/// <reference path="./defines/jquery.d.ts" />
/// <reference path="./defines/angular.d.ts" />
/// <reference path="./defines/custom.d.ts" />
/// <reference path="./shared/linearalgebra.ts" />
/// <reference path="./shared/svg_plots.ts" />
/// <reference path="./shared/wpf.ts" />
/// <reference path="./defines/ui-bootstrap.d.ts"/>
/// <reference path="./shared/models.ts"/>
/// <reference path="./shared/services.ts"/>
/// 
define(["require", "exports", "angular", "./shared/directives", './shared/wpf', 'ui-bootstrap', './shared/models', './shared/services'], function (require, exports, angular, directives, Interop, ui_bootstrap, dx, Services) {
    var d = typeof ui_bootstrap;
    var Ctrl = (function () {
        function Ctrl($wpf, $settings, $scope) {
            this.$wpf = $wpf;
            this.$settings = $settings;
            this.$scope = $scope;
            $scope.dirLight = new dx.DirectionalLightData(new dx.DXVector4(1, 0, 0, 0), new dx.DXVector4(0, 0, 0, 0), new dx.DXVector3(0, 0, 0), 0, new dx.DXVector3(1, 1, 1));
            $settings.SetObject("lights", $scope.dirLight);
        }
        Ctrl.prototype.set_lights = function () {
            //this.$scope
            this.$settings.Save("lights", this.$scope.dirLight);
            this.$wpf.postSync("cad/setlights", this.$scope.dirLight);
        };
        return Ctrl;
    })();
    var Test = (function () {
        function Test() {
        }
        return Test;
    })();
    var app = angular.module('app', ['ui.bootstrap']);
    app.controller('Ctrl', Ctrl);
    if (typeof (window.external.JavascriptRequest) != "undefined")
        app.service('$wpf', Interop.Wpf);
    else
        app.service('$wpf', Interop.MoqWpf);
    app.service('$settings', Services.Settings);
    directives.RegisterDirectives(app);
    //Register Fixtures
    Interop.MoqWpf.Fixtures["persistence/save"] = null;
    Interop.MoqWpf.Fixtures["persistence/load"] = null;
    var $html = angular.element(document.getElementsByTagName('html')[0]);
    angular.element().ready(function () {
        angular.bootstrap(document, ['app']);
    });
});
//# sourceMappingURL=lightcolor3d.js.map