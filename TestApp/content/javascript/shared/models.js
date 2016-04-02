define(["require", "exports"], function (require, exports) {
    var DXVector3 = (function () {
        function DXVector3(X, Y, Z) {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        return DXVector3;
    })();
    exports.DXVector3 = DXVector3;
    var DXVector4 = (function () {
        function DXVector4(X, Y, Z, W) {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.W = W;
        }
        return DXVector4;
    })();
    exports.DXVector4 = DXVector4;
    var DirectionalLightData = (function () {
        function DirectionalLightData(Ambient, Diffuse, Specular, SpecPower, Direction) {
            this.Ambient = Ambient;
            this.Diffuse = Diffuse;
            this.Specular = Specular;
            this.SpecPower = SpecPower;
            this.Direction = Direction;
        }
        return DirectionalLightData;
    })();
    exports.DirectionalLightData = DirectionalLightData;
});
//# sourceMappingURL=models.js.map