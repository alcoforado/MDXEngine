import { Component, OnInit } from '@angular/core';
import { ShapeUI, ShapesMngrService } from '../services/shapes-mngr-service';
import { MFormModel, UIType } from '../modules/mform/mformmodel';
import { ListViewItem } from '../list-view/list-view.component';
import { Observable } from 'rxjs/Observable'



export class RenderControl {
    data: any;
    renderType: UIType;
}
export class ShapeControl {
    public selectedRender: any;

    constructor(
        public shape: ShapeUI,
        public mform: MFormModel,
        public renders: Array<RenderControl>)
    { }
}

@Component({
    moduleId: module.id.toString(),
    selector: 'app-shapes-mngr',
    templateUrl: './shapes-mngr.component.html'
})
export class ShapesMngrComponent implements OnInit {

    ShapeTypes: Array<UIType> = [];
    RenderTypes: Observable<Array<UIType>> = null;
    shapes: Array<ShapeUI> = [];
    shapeControls: Array<ShapeControl> = [];
    shapeForms: Array<MFormModel>;
    showAddShapeDialog: boolean = false;
    shapesListView: Array<ListViewItem> = [];
    ngOnInit() {
        var all = Observable.zip(
            this.shapesMngrService.getTypesAsArray(),
            this.shapesMngrService.getShapes(),
            this.shapesMngrService.getRenderTypes()
        );
        var that = this;
        all.subscribe((elems) => {
            var [types, shapes, renderTypes] = elems;
            this.shapeControls = [];
            var rendersData = renderTypes.map(tp => {
                var obj = {};
                tp.Members.forEach((member) => {
                    obj[member.FieldName] = null;
                });
                return obj;
            });

            shapes.map((shape, i) => {
                return new ShapeControl(
                    shape,
                    new MFormModel(sh.ShapeData),



                );
            });

        });
        this.shapesMngrService.getTypesAsArray().combineAll()
        this.shapesMngrService.getTypesAsArray().subscribe(x => {
            this.ShapeTypes = x;
            this.shapesListView = this.ShapeTypes.map(
                (sh: UIType) => {
                    let result = new ListViewItem();
                    result.imageUrl = `/src/images/${sh.TypeName}.svg`,
                        result.itemLabel = sh.TypeName;
                    result.itemId = sh.TypeName;
                    return result;
                });
        });
        this.shapesMngrService.getShapes().subscribe(x => {
            this.shapes = x || [];
            this.shapeForms = this.shapes.map(sh => new MFormModel(sh.ShapeData));
        });
        this.RenderTypes = this.shapesMngrService.getRenderTypes();
    }

    constructor(private shapesMngrService: ShapesMngrService) { }

    disableAddShapeDialog(): void {
        this.showAddShapeDialog = false;
    }

    enableAddShapeDialog(): void {

        this.showAddShapeDialog = true;
    }

    createShape($event: ListViewItem) {
        this.shapesMngrService.createShape($event.itemId)
            .subscribe(x => {
                this.shapes.push(x)
                this.shapeForms.push(new MFormModel(x.ShapeData));
            });
        this.disableAddShapeDialog();
    }

}
