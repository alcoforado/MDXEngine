/// <reference path="./wpf.ts" />
define(["require", "exports"], function (require, exports) {
    var Settings = (function () {
        function Settings($wpf) {
            this.$wpf = $wpf;
        }
        Settings.prototype.DeepCopy = function (src, dst) {
            if (dst == null || src == null)
                return;
            for (var property in dst) {
                if (src.hasOwnProperty(property) && dst.hasOwnProperty(property)) {
                    if (typeof (src[property]) == "object" && typeof (dst[property] == "object")) {
                        this.DeepCopy(src[property], dst[property]);
                    }
                    else
                        dst[property] = src[property];
                }
            }
        };
        Settings.prototype.Save = function (key, object) {
            this.$wpf.postSync("persistence/save", { key: key, json: JSON.stringify(object) });
        };
        Settings.prototype.Load = function (key) {
            var json = this.$wpf.postSync("persistence/load", { key: key });
            var value = JSON.parse(json);
            return value;
        };
        Settings.prototype.SetObject = function (key, object) {
            var json = this.$wpf.postSync("persistence/load", { key: key });
            var value = JSON.parse(json);
            this.DeepCopy(value, object);
            return object;
        };
        return Settings;
    })();
    exports.Settings = Settings;
});
//# sourceMappingURL=services.js.map