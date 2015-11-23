/// <reference path="./defines/angular.d.ts" />
/// <reference path="./defines/jquery.d.ts" />
/// <reference path="./templates.ts" />
/// 
import templates = require("templates");
import angular = require("angular");
import la = require("linearalgebra");
import $ = require("jquery");


interface IVectorPickerScope extends ng.IScope {
    RotXY: number;
    RotXYZ: number;
    CssRotXYZ: number;
    CssRotXY: number;
}

export class VectorPicker implements angular.IDirective
{
    template: string;
        restrict: string;
    
    link: ng.IDirectiveLinkFn;

    scope: any;

    //scope: any;
    constructor()
    {
        this.restrict = 'E';
        this.template = templates.vectorpicker;
        this.scope = true;
        
       
        this.link = (scope: IVectorPickerScope, element: ng.IAugmentedJQuery, attributes: ng.IAttributes) =>
       {
           var dim = 100;
           
           var rotXYZ = 0;

           var xy = element.find("#xy");
           var xyz = element.find("#xyz");
          
           scope.RotXY = 0;
           scope.RotXYZ = 0;
           scope.CssRotXYZ = 0;
           scope.CssRotXY = 0;


           scope.$watch('RotXY',(newValue:number,oldValue:number,scope:IVectorPickerScope) => {
               var rotxy = scope.RotXY;
               scope.CssRotXY = 360 - rotxy;
               xy.css('transform', "rotate(" + scope.CssRotXY + "deg)");
           });

           scope.$watch('RotXYZ',(newValue: number, oldValue: number, scope: IVectorPickerScope) => {
               var rotxyz = scope.RotXYZ;
               scope.CssRotXYZ = 360 - rotxyz;
               xyz.css('transform', "rotate(" + scope.CssRotXYZ + "deg)");
           });


           xy.on('mousedown',(event: JQueryEventObject) => {
               var v = new la.Vec2([event.offsetX, event.offsetY]);
               var o = new la.Vec2([dim / 2.0, dim / 2.0]);
               var d = v.sub(o);
               var that = $(event.target)
               event.preventDefault();
               var angle = d.angleDeg();
               //alert("Angle: " + angle + "deg =>" + rot);
               scope.CssRotXY += angle;
               scope.CssRotXY = scope.CssRotXY % 360;
               that.css('transform', "rotate(" + scope.CssRotXY + "deg)");
               scope.RotXY = parseFloat((360   - scope.CssRotXY %360).toFixed(0));
               scope.$apply();
               //element[0].style.width = '400px';
           });


           xyz.on('mousedown',(event: JQueryEventObject) => {
               var v = new la.Vec2([event.offsetX, event.offsetY]);
               var o = new la.Vec2([dim / 2.0, dim / 2.0]);
               var d = v.sub(o);
               var that = $(event.target)
               event.preventDefault();
               var angle = d.angleDeg();
               scope.CssRotXYZ += angle;
               scope.CssRotXYZ = scope.CssRotXYZ % 360;
               that.css('transform', "rotate(" + scope.CssRotXYZ + "deg)");
               scope.RotXYZ = parseFloat((360 - scope.CssRotXYZ % 360).toFixed(0));
               scope.$apply();


              
           });

           


          

      
               

               //alert("P: "+ event.offsetX +"," + event.offsetY + "X: ");
           }
       }
       // this.scope = true;
}



export function RegisterDirectives(app: angular.IModule) {
    app.directive('vectorPicker', () => new VectorPicker())
}
