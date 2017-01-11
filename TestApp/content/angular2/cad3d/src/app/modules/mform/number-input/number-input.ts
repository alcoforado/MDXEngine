import {Component,Input} from '@angular/core';
import { MFormComponent,MFormModel,IMFormModel } from "./../mformmodel";
import {FormGroup,FormControl,AbstractControl} from '@angular/forms'
@Component({
    moduleId: module.id.toString(),
    selector: 'number-input',
    templateUrl: './number-input.component.html',
})
export class NumberInputComponent  {
    @Input() formComponent: MFormComponent;
    @Input() model:IMFormModel;
    @Input() field:string;
    @Input() validations: string;
    @Input() control:FormGroup;
    test:string="first";
    ngOnInit() {
        this.formComponent = this.model.getFormComponent(this.field);
        this.control= this.formComponent.group;
    }

}

