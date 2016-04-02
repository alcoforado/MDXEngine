define(["require", "exports", "templates", "linearalgebra", "jquery"], function (require, exports, templates, la, $) {
    var VectorPicker = (function () {
        /*
            updateDialog(scope: IVectorPickerScope) {
                var xy =
            }
            */
        //scope: any;
        function VectorPicker() {
            this.restrict = 'E';
            this.template = templates.vectorpicker;
            this.scope = true;
            this.link = function (scope, element, attributes) {
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
                scope.displayDialog = function (ev) {
                    scope.showDialog = true;
                };
                scope.hideDialog = function (ev) {
                    scope.showDialog = false;
                };
                scope.$watch('RotXY', function (newValue, oldValue, scope) {
                    var rotxy = scope.RotXY;
                    scope.CssRotXY = 360 - rotxy;
                    xy.css('transform', "rotate(" + scope.CssRotXY + "deg)");
                    VectorPicker.updateInput(scope);
                });
                scope.$watch('RotXYZ', function (newValue, oldValue, scope) {
                    var rotxyz = scope.RotXYZ;
                    scope.CssRotXYZ = 360 - rotxyz;
                    xyz.css('transform', "rotate(" + scope.CssRotXYZ + "deg)");
                    VectorPicker.updateInput(scope);
                });
                scope.$watch('norm', function (newValue, oldValue, scope) {
                    VectorPicker.updateInput(scope);
                });
                scope.$watch('vector0 + vector1 + vector2', function (newValue, oldValue, scope) {
                    VectorPicker.updateControl(scope);
                });
                var re = new RegExp("<\\d+.(?=\\d{1,3}),\\d+.(?=\\d{1,3}),\\d+.(?=\\d{1,3})>");
                element[0].addEventListener("mousewheel", function (ev) {
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
                xy.on('mousedown', function (event) {
                    var v = new la.Vec2([event.offsetX, event.offsetY]);
                    var o = new la.Vec2([dim / 2.0, dim / 2.0]);
                    var d = v.sub(o);
                    var that = $(event.target);
                    event.preventDefault();
                    var angle = d.angleDeg();
                    //alert("Angle: " + angle + "deg =>" + rot);
                    scope.CssRotXY += angle;
                    scope.CssRotXY = scope.CssRotXY % 360;
                    that.css('transform', "rotate(" + scope.CssRotXY + "deg)");
                    scope.RotXY = parseFloat((360 - scope.CssRotXY % 360).toFixed(0));
                    scope.$apply();
                    //element[0].style.width = '400px';
                });
                xyz.on('mousedown', function (event) {
                    var v = new la.Vec2([event.offsetX, event.offsetY]);
                    var o = new la.Vec2([dim / 2.0, dim / 2.0]);
                    var d = v.sub(o);
                    var that = $(event.target);
                    event.preventDefault();
                    var angle = d.angleDeg();
                    scope.CssRotXYZ += angle;
                    scope.CssRotXYZ = scope.CssRotXYZ % 360;
                    that.css('transform', "rotate(" + scope.CssRotXYZ + "deg)");
                    scope.RotXYZ = parseFloat((360 - scope.CssRotXYZ % 360).toFixed(0));
                    scope.$apply();
                });
                //alert("P: "+ event.offsetX +"," + event.offsetY + "X: ");
            };
        }
        VectorPicker.updateInput = function (scope) {
            var v = (new la.Vec3([scope.norm, la.Angle.toRad(scope.RotXY), la.Angle.toRad(scope.RotXYZ)]));
            scope.vector0;
            scope.vector1;
            scope.vector2;
            var x = v[0] * Math.cos(v[1]) * Math.cos(v[2]);
            var y = v[0] * Math.sin(v[1]) * Math.cos(v[2]);
            var z = v[0] * Math.sin(v[2]);
            if (la.gl_equal(x, scope.vector0) && la.gl_equal(y, scope.vector1) && la.gl_equal(z, scope.vector2))
                return;
            if (Math.abs(x) < 1.e-10)
                x = 0.0;
            if (Math.abs(y) < 1.e-10)
                y = 0.0;
            if (Math.abs(z) < 1.e-10)
                z = 0.0;
            scope.vector0 = x;
            scope.vector1 = y;
            scope.vector2 = z;
        };
        VectorPicker.updateControl = function (scope) {
            var v = (new la.Vec3([scope.vector0, scope.vector1, scope.vector2]));
            scope.norm = Math.sqrt(scope.vector0 * scope.vector0 + scope.vector1 * scope.vector1 + scope.vector2 * scope.vector2);
            scope.RotXYZ = Math.asin(scope.vector2 / scope.norm);
            if (scope.RotXYZ < 0.0)
                scope.RotXYZ = scope.RotXYZ + Math.PI * 2;
            scope.RotXYZ = la.Angle.toDeg(scope.RotXYZ);
            var hypXY = Math.sqrt(scope.vector0 * scope.vector0 + scope.vector1 * scope.vector1);
            if (la.gl_equal(hypXY, 0)) {
                scope.RotXY = 0;
            }
            else {
                scope.RotXY = Math.acos(scope.vector0 / hypXY);
                if (scope.vector1 < 0)
                    scope.RotXY = Math.PI * 2 - scope.RotXY;
                scope.RotXY = la.Angle.toDeg(scope.RotXY);
            }
        };
        return VectorPicker;
    })();
    exports.VectorPicker = VectorPicker;
    function RegisterDirectives(app) {
        app.directive('vectorPicker', function () { return new VectorPicker(); });
    }
    exports.RegisterDirectives = RegisterDirectives;
});
//# sourceMappingURL=directives.js.map