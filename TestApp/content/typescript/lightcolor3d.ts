/// <reference path="./defines/jquery.d.ts" />
/// <reference path="./defines/angular.d.ts" />
/// <reference path="./defines/custom.d.ts" />
/// <reference path="./shared/linearalgebra.ts" />
/// <reference path="./shared/svg_plots.ts" />
/// <reference path="./shared/wpf.ts" />
/// <reference path="./defines/ui-bootstrap.d.ts"/>
/// <reference path="./shared/models.ts"/>


import la = require("./shared/linearalgebra");
import angular = require("angular");
import directives = require("./shared/directives");
import Interop = require('./shared/wpf');
import ui_bootstrap = require('ui-bootstrap');
import dx = require('./shared/models');
var d = typeof ui_bootstrap;




interface IScope extends ng.IScope {
    dirLight: dx.DirectionalLightData

}

class Ctrl {



    constructor(private $wpf: Interop.Wpf, private $scope: IScope) {
        $scope.dirLight = new dx.DirectionalLightData(
            new dx.DXVector4(0, 0, 0, 0),
            new dx.DXVector4(0, 0, 0, 0),
            new dx.DXVector3(0, 0, 0),
            0,
            new dx.DXVector3(1, 1, 1));
    }

    set_lights() {
        //this.$scope
        this.$wpf.postSync("CadController/SetLights", this.$scope.dirLight);
    }

}

class Test {
    constructor() { }
}



var app = angular.module('app', ['ui.bootstrap']);
app.controller('Ctrl', Ctrl);

if (typeof ((<any> window.external).JavascriptRequest) != "undefined")
    app.service('$wpf', Interop.Wpf);
else
    app.service('$wpf', Interop.MoqWpf);

directives.RegisterDirectives(app);
var $html = angular.element(document.getElementsByTagName('html')[0]);
angular.element().ready(function () {
    angular.bootstrap(document, ['app']);
});


