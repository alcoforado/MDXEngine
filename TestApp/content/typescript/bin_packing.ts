/// <reference path="./defines/jquery.d.ts" />
/// <reference path="./defines/angular.d.ts" />
/// <reference path="./defines/custom.d.ts" />
/// <reference path="./linearalgebra.ts" />
/// <reference path="./svg_plots.ts" />



import plots = require("svg_plots");
import la = require("linearalgebra");
import angular = require("angular");
import shapes2D = require("shapes2d");



 class Data {
        constructor(public NumElements: number, public WidthRange: la.Interval,public HeightRange: la.Interval, public numberOfRuns=1,public rotate:boolean=false, public areaUsageValues:Array<number>=[])
        {
            
        }
    }

   interface IScope extends ng.IScope {
       Data: Data;

       Run: any;
    }

   class Ctrl {


        constructor(private $scope: IScope) {
            $scope.Data = new Data(50, new la.Interval(10, 100), new la.Interval(10, 100));
        }

        plotEfficiencyGraph(y:Array<number>) {
            var y = this.$scope.Data.areaUsageValues;

            if (y.length <= 1)
                $(".efficiency-graph-container").html("");
            else {

                var options: plots.PlotOptionsBase = {
                    height: 369,
                    width: 545,
                    line_width: 0,
                    points_size: 2,
                    points_type: 'circle',
                    color: 'purple',
                    padding: [5, 5, 5, 5],
                    font_size: 8,
                    yrange: [0, 100]
                };
                var x:Array<number> = [];
                for (var i= 0; i < y.length;i++)
                {
                    x.push(i);
                }
                var p = new plots.SvgPlot();
                p.plot2d($(".efficiency-graph-container")[0], x, y, options);

            }

        }

        Run() {
            var i = 0;
            var that = this;
            var data = this.$scope.Data;
            if (data.numberOfRuns <= 1) {
                this.RunOnce();
            }
            else {
                var interval = setInterval(function () {
                    that.RunOnce();
                    if (++i >= data.numberOfRuns) {
                        clearInterval(interval);
                    }
                }, 100);

            }

        } 


        RunOnce() {
            this.plotEfficiencyGraph(this.$scope.Data.areaUsageValues);
            
            }


        
            
       

        RunAgain() {
           
        }

    }
    


   

///////////////////////////////////////////////////////////////////////////////////////////////////
//Angular initialization with require.js
/////////////////////////////////////////////////////////////////////////////////////////////////

    var app = angular.module('app', []);
    app.controller('Ctrl', Ctrl);

    var $html = angular.element(document.getElementsByTagName('html')[0]);
    angular.element().ready(function () {
        // bootstrap the app manually
        angular.bootstrap(document, ['app']);
    });


    //$(function () {
    //    var p = new plots.SvgPlot();


    //    var options: plots.PlotOptionsBase = {
    //        height: 369,
    //        width: 545,
    //        line_width: 0,
    //        points_size: 5,
    //        points_type: 'circle',
    //        color: 'purple',
    //        padding: [5, 5, 5, 5],
    //        font_size: 8,
    //        yrange:[0,500]
    //    };
    //    var x = [1, 2, 3, 4 , 5];
    //    var y = [100, 200, 300,400,500];

    //   // p.plot2d($(".efficiency-graph-container")[0],x,y, options);

    //});
