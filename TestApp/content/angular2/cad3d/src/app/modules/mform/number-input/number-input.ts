import {Component,Input} from '@angular/core';
@Component({
    moduleId: module.id.toString(),
    selector: 'number-input',
    templateUrl: './number-input.component.html',
})
export class NumberInputComponent  {
    @Input() value: number;
    @Input() validations: string;
}

