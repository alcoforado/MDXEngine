"use strict";
var forms_1 = require("@angular/forms");
var MFormComponent = (function () {
    function MFormComponent(component, isAGroup) {
        this.component = component;
        this.isAGroup = isAGroup;
    }
    MFormComponent.prototype.isGroup = function () {
        return this.isAGroup;
    };
    MFormComponent.prototype.getFormGroup = function () {
        if (this.isGroup)
            return this.component;
        else
            throw "MFormCompoent doesn't have a group";
    };
    MFormComponent.prototype.getFormControl = function () {
        if (!this.isGroup)
            return this.component;
        else
            throw "MFormComponent doesn't have a form control";
    };
    return MFormComponent;
}());
exports.MFormComponent = MFormComponent;
var MFormModel = (function () {
    function MFormModel(model) {
        this.model = model;
        this.root = new forms_1.FormGroup({});
    }
    MFormModel.prototype.getRoot = function () { return this.root; };
    MFormModel.prototype.createFormComponent = function (propertyName) {
        var type = typeof (this.model[propertyName]).toLowerCase();
        switch (type) {
            case "undefined":
                throw "Error, propertyName " + propertyName + " does not exist";
            case "number":
            case "string":
            case "boolean":
                {
                    var child = new forms_1.FormControl(this.model[propertyName]);
                    this.root.addControl(propertyName, child);
                    return new MFormComponent(child, false);
                }
            case "object":
                {
                    var child = new forms_1.FormGroup(this.model[propertyName]);
                    this.root.addControl(propertyName, child);
                    return new MFormComponent(child, true);
                }
            default:
                throw "Not valid type " + type + " for property " + propertyName;
        }
    };
    return MFormModel;
}());
exports.MFormModel = MFormModel;
//# sourceMappingURL=mformmodel.js.map