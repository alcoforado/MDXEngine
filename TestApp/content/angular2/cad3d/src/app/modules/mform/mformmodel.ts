import {FormGroup,FormControl,AbstractControl} from '@angular/forms'


export class MFormComponent {

    constructor(public group:FormGroup,public name:string,private isPrimitiveType:boolean) { }

    isPrimitive(): boolean {
        return this.isPrimitiveType;
    }


}

export interface IMFormModel
{
    getFormComponent(propertyName: string): MFormComponent;

}


export class MFormModel<T> {

    root: FormGroup;

    _formComponentCash: {[id:string] : MFormComponent} = {};
    constructor(public model: T) {
        this.root = new FormGroup({});
    }
    getRoot(): FormGroup { return this.root; }

    getFormComponent(propertyName: string): MFormComponent {

        var type: string = typeof (this.model[propertyName]).toString().toLowerCase();


        switch (type) {
            case "undefined":
                throw `Error, propertyName ${propertyName} does not exist`;
            case "number":
            case "string":
            case "boolean":
                {
                    if (typeof(this._formComponentCash[propertyName])!=="undefined")
                    {
                        return this._formComponentCash[propertyName];
                    }


                    

                    var child = new FormControl(this.model[propertyName]);
                   // this.root.addControl(propertyName, child);
                    
                    var enclosureGroup= new FormGroup({});
                    enclosureGroup.addControl(propertyName,child);
                    this._formComponentCash[propertyName] =  new MFormComponent(enclosureGroup,propertyName, true);
                    return this._formComponentCash[propertyName];
                }
            case "object":
                {
                    let child = new FormGroup(this.model[propertyName]);
                    this.root.addControl(propertyName, child);
                    return new MFormComponent(child, propertyName, false);
                }
            default:
                throw `Not valid type ${type} for property ${propertyName}`;
        }
    }

    


}