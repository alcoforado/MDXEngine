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
    norm: number;
    vector0: number;
    vector1: number;
    vector2: number;
    showDialog: boolean;
    displayDialog(ev: any);
    hideDialog(ev:any);

}

export class VectorPicker implements angular.IDirective
{
    template: string;
        restrict: string;
    
    link: ng.IDirectiveLinkFn;

    scope: any;


    updateInput(scope: IVectorPickerScope) {
        scope.vector2 = Math.cos(scope.RotXYZ) * scope.norm;
        scope.vector1 = Math.sin(scope.RotXYZ) * scope.norm * Math.sin(scope.RotXY);
        scope.vector0 = Math.sin(scope.RotXYZ) * scope.norm * Math.cos(scope.RotXY);
    }
/*
    updateDialog(scope: IVectorPickerScope) {
        var xy = 
    }
    */
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
           scope.norm = 1;
           scope.showDialog = false;

           scope.displayDialog = (ev:any) => {
               scope.showDialog = true;
           }
           scope.hideDialog = (ev: any) => {
               scope.showDialog = false;
           }

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

           var re = new RegExp("<\\d+.(?=\\d{1,3}),\\d+.(?=\\d{1,3}),\\d+.(?=\\d{1,3})>");

           element[0].addEventListener("mousewheel", function (ev: any) {
               if (ev.wheelDelta > 0) {
                   scope.norm *= 1.1;
                   scope.$apply();
               }
               else if (ev.wheelDelta < 0) {
                   scope.norm *= 0.9;
                   scope.$apply();
               }
               ev.preventDefault();
                
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
