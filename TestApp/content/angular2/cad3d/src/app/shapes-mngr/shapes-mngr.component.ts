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
        this.shapesMngrService.getTypesAsArray().subscribe(x => {
            this.shapeTypes = x;
            this.shapesListView = this.shapeTypes.map(
                (sh:ShapeType)=> { 
                    let result = new ListViewItem();
                    result.imageUrl = `/src/images/${sh.TypeName}.svg`,
                    result.itemLabel=sh.TypeName;
                    result.itemId=sh.TypeName;
                    return result;
                });
        });

        this.shapesMngrService.getShapes().subscribe(x => {
            this.shapes = x || [];
            this.shapeForms = this.shapes.map(sh => new MFormModel(sh.ShapeData));
            
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

    createShape($event:ListViewItem)
    {
        this.shapesMngrService.createShape($event.itemId)
            .subscribe(x=>{
                this.shapes.push(x)
                this.shapeForms.push(new MFormModel(x.ShapeData));
            });
        this.disableAddShapeDialog();


    }

}
