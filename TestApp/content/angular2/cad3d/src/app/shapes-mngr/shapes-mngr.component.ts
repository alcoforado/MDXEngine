import { Component, OnInit } from '@angular/core';
import {ShapeUI,ShapeType,ShapesMngrService}  from '../services/shapes-mngr-service';
import {MFormModel} from '../modules/mform/mformmodel'
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
    ngOnInit() {
        this.shapesMngrService.GetTypesAsArray().subscribe(x => this.shapeTypes = x);

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
