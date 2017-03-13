import { Component, OnInit,Input } from '@angular/core';
import { NgModule } from '@angular/core';
import * as $ from 'jquery'

interface IDropBoxItem {
  label: string

}

@Component({
  moduleId: module.id.toString(),
  selector: 'app-drop-box',
  templateUrl: './drop-box.component.html',
})
export class DropBoxComponent implements OnInit {
  isExpanded: boolean = false;
  selectedIndex:number = 0;
  @Input() items: Array<IDropBoxItem>=null;

  constructor() { }

  ngOnInit() {
    if (this.items==null)
    {
      this.items=[{label:"United States"},{label:"Iraq"},{label:"Vietnam"},{label:"North Korea"}];
    }
  }
  
  clicked()
  {
    this.isExpanded = !this.isExpanded;
  }


}


