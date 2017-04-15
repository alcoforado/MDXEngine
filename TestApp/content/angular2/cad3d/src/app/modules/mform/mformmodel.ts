import { FormGroup, FormControl, AbstractControl } from '@angular/forms'


export class MFormComponent {

    constructor(public parent: MFormModel, public name: string) { }

    setValue(obj: any) {
        if (obj == null)
            throw "MFormComponent needs a non null object"
        var type = typeof (obj);
        switch (type) {
            case "string":
            case "number":
            case "boolean":


        }

    }

}

export interface IMFormModel {
    getFormComponent(propertyName: string): MFormComponent;

}

export class TypeMember {
    constructor(
        public FieldName: string,
        public LabelName: string,
        public DirectiveType: string,
        public JavascriptType: string
    ) { }
}

export class UIType {
    static primitiveTypes: string[] = ["number", "int", "bool", "string"];
    constructor(
        public TypeName: string,
        public Members: Array<TypeMember>,
        public TypeLabel: string
    ) { }
}


export class MFormModel {

    root: FormGroup;
    _formComponentCash: { [id: string]: MFormComponent } = {};
    constructor(public model: any, public type: UIType = null) {
        this.root = new FormGroup({});
    }
    getRoot(): FormGroup { return this.root; }


    getFormComponent(propertyName: string): MFormComponent {

        if (typeof (this._formComponentCash[propertyName]) !== "undefined") {
            return this._formComponentCash[propertyName];
        }




        //var child = new FormControl(this.model[propertyName]);
        // this.root.addControl(propertyName, child);

        //var enclosureGroup= new FormGroup({});
        this._formComponentCash[propertyName] = new MFormComponent(this, propertyName);
        return this._formComponentCash[propertyName];

    }
}




