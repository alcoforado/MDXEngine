import { FormGroup, FormControl, AbstractControl } from '@angular/forms'

//7274653768
export class MFormComponent {
    formControl: FormControl = null;
    formGroup: FormGroup = null;
    constructor(public parent: MFormModel, public name: string) { }

    setValue(obj: any) {
        if (obj == null)
            throw "MFormComponent needs a non null object"

        var type = typeof (obj);
        switch (type) {
            case "string":
            case "number":
            case "boolean":
                if (this.formGroup != null) {
                    throw "The type of value passed to the method setValue does not match the one of preveous call"
                }
                if (this.formControl == null) {
                    this.formControl = new FormControl();
                    this.formControl.setValue(obj);
                    this.parent.getRoot().addControl(this.name, this.formControl);
                }
                else { //formControl already initialized, just set the value
                    this.formControl.setValue(obj);
                }
                break;
            case "object":
                if (this.formControl != null) {
                    throw "The type of value passed to the method setValue does not match the one of a preveous call"
                }
                if (this.formGroup == null) {
                    this.formGroup = new FormGroup(obj);
                    this.parent.getRoot().addControl(this.name, this.formControl);
                }
                else { //formControl already initialized, just set the value
                    this.formControl.setValue(obj);
                }


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




