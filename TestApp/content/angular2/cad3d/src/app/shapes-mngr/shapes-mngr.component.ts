import { Component, OnInit } from '@angular/core';
import {ShapeUI,ShapeType,ShapesMngrService}  from '../services/shapes-mngr-service';
import {MFormModel} from '../modules/mform/mformmodel';
import {ListViewItem} from '../list-view/list-view.component';
@Component({
  moduleId: module.id.toString(),
  selector: 'app-shapes-mngr',
  templateUrl: './shapes-mngr.component.html'
})
export class ShapesMngrComponent implements OnInit {

    shapeTypes: Array<ShapeType> = [];
    shapes:Array<ShapeUI>=[]
    shapeForms: Array<MFormModel<ShapeUI>>; 
    showAddShapeDialog:boolean=false;
    shapesListView:Array<ListViewItem>=[];
    ngOnInit() {
        this.shapesMngrService.GetTypesAsArray().subscribe(x => {
            this.shapeTypes = x;
            this.shapesListView = this.shapeTypes.map(
                (sh:ShapeType)=> { 
                    let result = new ListViewItem();
                    result.imageUrl = sh.typeName+".svg",
                    result.itemLabel=sh.typeName;
                    result.itemId=sh.typeName;
                    return result;
                });
        });

        this.shapesMngrService.GetShapes().subscribe(x => {
            this.shapes = x;
            this.shapeForms = this.shapes.map(sh => new MFormModel(sh.shapeData));
            
        });

       

    }

    constructor(private shapesMngrService: ShapesMngrService) {}

    disableAddShapeDialog():void
    {
        this.showAddShapeDialog=false;
    }

    enableAddShapeDialog():void
    {
        
        this.showAddShapeDialog=true;
    }

}
