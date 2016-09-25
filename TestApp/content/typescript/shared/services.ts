/// <reference path="./wpf.ts" />
/// <reference path="../defines/angular.d.ts" />


import Interop = require('./wpf');
 

export class Settings {

    constructor(private $wpf: Interop.Wpf) {


    }

    private DeepCopy(src: any, dst: any) {
        if (dst == null || src== null)
            return;
        for (var property in dst) {
            if (src.hasOwnProperty(property) && dst.hasOwnProperty(property)) {
                if (typeof (src[property]) == "object" && typeof (dst[property] == "object")) {
                    this.DeepCopy(src[property], dst[property])
                }
                else
                    dst[property] = src[property];
            }
        }
    }

    public Save(key:string, object: any) {
        this.$wpf.postSync("persistence/save", { key: key, json: JSON.stringify(object) });
    }

    public Load(key:string): any {
        var json = <string> this.$wpf.postSync("persistence/load", { key: key });
        var value = JSON.parse(json);
        return value;
    }

    public SetObject(key: string, object: any) {
        var json = <string> this.$wpf.postSync("persistence/load", { key: key });
        var value = JSON.parse(json);
        this.DeepCopy(value, object);
        return object;
    }



}

export class ShapeUI {
    constructor(
        public shapeType: string,
        public shapeData: any,
        public type: ShapeType
    ) {
    }
}

export class ShapeType {
    constructor(
        public typeName: string,
        public members: Array<ShapeMember>
    ) { }
}

export class ShapeMember {
    constructor(
        public fieldName: string,
        public labelName: string,
        public directiveType: string
    ) { }
}



export class ShapeMngr {
    private Types: { [typeName: string]: ShapeType }=null;

    constructor(private $wpf: Interop.Wpf)
    {


    }



    GetTypes(): { [typeName: string]: ShapeType } {
        if (this.Types == null) {
            this.Types = {};
            var value: Array<ShapeType> = this.$wpf.postSync("shapesmngr/gettypes");
            value.forEach((elem) => {
                this.Types[elem.typeName] = elem;
            });
        }
        return this.Types;
    }

    GetType(name: string): ShapeType {
        if (typeof (this.GetTypes()[name]) == "undefined")
            throw "Type " + name + " not found";
        else {
            return this.Types[name];
        }
    }

    GetShapes():Array<ShapeUI> {
        var value:Array<ShapeUI> = this.$wpf.postSync("shapesmngr/getshapes");
        value.forEach((elem) => {
            elem.type = this.GetType(elem.shapeType);
        });

        return value;
    }







        
    


}


export function registerServices(app: angular.IModule) {
    app.service('$settings', Settings);
    app.service('$shapesMngr', ShapeMngr);
   
}