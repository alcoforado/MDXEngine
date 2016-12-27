import { Http, Response } from '@angular/http'
import {Observable} from 'rxjs/Observable'

export class ShapeUI {
    constructor(
        public typeName: string,
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
    private Types: { [typeName: string]: ShapeType } = null;

    private TypesArray: Array<ShapeType>;

    constructor(private $http: Http) {


    }


    GetTypes(): { [typeName: string]: ShapeType } {
        if (this.Types == null) {
            this.Types = {};
            this.TypesArray = this.$wpf.postSync("shapesmngr/gettypes");
            this.TypesArray.forEach((elem) => {
                this.Types[elem.typeName] = elem;
            });
        }
        return this.Types;
    }

    GetTypesAsArray(): Array<ShapeType> {
        this.GetTypes();
        return this.TypesArray;
    }

    GetType(name: string): ShapeType {
        if (typeof (this.GetTypes()[name]) == "undefined")
            throw "Type " + name + " not found";
        else {
            return this.Types[name];
        }
    }

    GetShapes(): Array<ShapeUI> {
        var value: Array<ShapeUI> = this.$wpf.postSync("shapesmngr/getshapes");
        value.forEach((elem) => {
            elem.type = this.GetType(elem.typeName);
        });

        return value;
    }

    CreateShape(type: ShapeType): ShapeType {
        return this.$wpf.postSync("shapesmngr/createShape",
            {});
    }
}


