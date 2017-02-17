import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery'
@Component({
  moduleId: module.id.toString(),
  selector: 'app-drop-box',
  templateUrl: './drop-box.component.html',
})
export class DropBoxComponent implements OnInit {
  isExpanded: boolean = false;
  
  constructor() { }

  ngOnInit() {
  }
  
  clicked()
  {
    this.isExpanded = !this.isExpanded;
  }
}


