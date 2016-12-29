﻿import {Component,OnInit} from '@angular/core';
import { InMemoryWebApiModule } from 'angular-in-memory-web-api';
import {HttpModule} from '@angular/http'
import {ShapesMngrService,ShapeType,ShapeUI} from '../services/shapes-mngr-service'
@Component({
    moduleId: module.id,
    selector: 'shapes-mngr',
    templateUrl: 'shapes-mngr.component.html',
    providers: [ShapesMngrService]
})
    export class ShapesMngrComponent implements OnInit {

    shapeTypes: Array<ShapeType> = [];
    shapes:Array<ShapeUI>=[]

    ngOnInit() {
        this.shapesMngrService.GetTypesAsArray().subscribe(x => this.shapeTypes = x);

        this.shapesMngrService.GetShapes().subscribe(x =>
            this.shapes = x
        );
    }

    constructor(private shapesMngrService: ShapesMngrService) {

    }






}
