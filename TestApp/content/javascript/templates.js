define(["require", "exports"], function (require, exports) {
    "use strict";
    exports.shape = '\
<div class="form-header"><img src="../images/shapes.svg" />{{shape.type.typeName}}</div>\
<div class="form-body">\
    <div class="form-row" ng-repeat="field in shape.type.members">\
        {{field.directiveType}} {{field.labelName}}\
    </div>\
</div>';
    exports.vectorpicker = '\
\
<div class="vector-input-container">\
    <input class="vector-input" type="number" ng-model="vector.X" />\
    <input class="vector-input" type="number" ng-model="vector.Y" />\
    <input class="vector-input" type="number" ng-model="vector.Z" />\
    <div class="dialog-button" ng-click="displayDialog()"></div>\
\
\
\
    <div class="screen-container" ng-show="showDialog">\
        <div class="left-pane">\
            <div class="circle-background" />\
            <div id="xy" class="vector-picker" />\
            <span>Angle: </span><input ng-model="RotXY" type="number" min="0" max="99999" value="" />\
        </div>\
\
        <div class="right-pane">\
            <div class="circle-background-xyz" />\
            <div id="xyz" class="vector-picker">\
            </div>\
            <span>Angle: </span><input ng-model="RotXYZ" type="number" min="0" max="99999" value="1" />\
        </div>\
        <br />\
        <div class="norm-pane">\
            <span>Norm: </span><input ng-model="norm" type="number" size="10" min="0" max="99999" value="1" />\
        </div>\
        <div class="float-right">\
            <div class="button-flat" ng-click="hideDialog()">Ok</div>\
        </div>\
    </div>\
</div>\
\
\
';
});
