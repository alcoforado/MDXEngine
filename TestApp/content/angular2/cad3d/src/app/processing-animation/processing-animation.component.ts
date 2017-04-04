import { Component, OnInit, Input } from '@angular/core';

@Component({
    moduleId: module.id,
    selector: 'processing-animation',
    templateUrl: 'processing-animation.component.html'
})
export class ProcessingAnimation implements OnInit {
    @Input() text: string = "processing";
    @Input() animation: string = "horizontal-led";
    constructor() { }

    ngOnInit() { }

}