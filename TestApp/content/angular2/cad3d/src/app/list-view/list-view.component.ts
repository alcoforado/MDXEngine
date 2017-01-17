import { Component, OnInit,Input } from '@angular/core';


export interface IListViewItem
{
  imageUrl:string;
  itemId:string;
  itemLabel:string;
}


@Component({
  moduleId: module.id,
  selector: 'app-list-view',
  templateUrl: './list-view.component.html'
})
export class ListViewComponent implements OnInit {
  @Input() items:Array<IListViewItem> 
  @Input() defaultImage:string;
  constructor() { }

  ngOnInit() {
  }

}
