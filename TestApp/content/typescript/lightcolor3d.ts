/// <reference path="./defines/jquery.d.ts" />
/// <reference path="./defines/angular.d.ts" />
/// <reference path="./defines/custom.d.ts" />
/// <reference path="./linearalgebra.ts" />
/// <reference path="./svg_plots.ts" />
/// <reference path="./wpf.ts" />
/// <reference path="./defines/ui-bootstrap.d.ts"/>

import la = require("linearalgebra");
import angular = require("angular");
import Interop = require('wpf');
import ui_bootstrap = require('ui-bootstrap');
var d = typeof ui_bootstrap;
class Data {
    constructor(public NumElements: number
        , public minWidth: number
        , public maxWidth: number
        , public minHeight: number
        , public maxHeight: number
        , public numberOfRuns = 1
        , public rotate: boolean = false
        , public areaUsageValues: Array<number> = []) {

    }
}

interface IScope extends ng.IScope {
    Data: Data;

    Run: any;
}

class Ctrl {
    constructor(private $wpf: Interop.Wpf, private $scope: IScope) {
        $scope.Data = new Data(50, 10, 100, 10, 100);
    }
}
var app = angular.module('app', ['ui.bootstrap']);
app.controller('Ctrl', Ctrl);
app.service('$wpf', Interop.Wpf);
var $html = angular.element(document.getElementsByTagName('html')[0]);
angular.element().ready(function () {
    angular.bootstrap(document, ['app']);
});