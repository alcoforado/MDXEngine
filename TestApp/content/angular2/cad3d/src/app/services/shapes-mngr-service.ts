import { Http, Response } from '@angular/http'
import 'rxjs/Rx'
import {Observable} from 'rxjs/Observable'
import {InMemMockService} from './mocks'
import { Injectable } from '@angular/core';




export class ShapeType {
    constructor(
        public TypeName: string,
        public Members: Array<ShapeMember>
    ) { }
}

export class ShapeUI {
    constructor(
        public TypeName: string,
        public ShapeData: any,
        public Type: ShapeType
    ) {
    }
}

export class ShapeMember {
    constructor(
        public FieldName: string,
        public LabelName: string,
        public DirectiveType: string
    ) { }
}

@Injectable()
export class ShapesMngrService {
    private TypesHash: Observable<{ [typeName: string]: ShapeType }> = null;
    private Types: Observable<Array<ShapeType>> = null;

    private extractData<T>(res: Response): T {
        let body = res.json();
        return <T>(body || {});
    }


    constructor(private $http: Http) {


    }



    getTypes(): Observable<{ [typeName: string]: ShapeType }> {
        if (this.Types == null) {
            this.Types = this.$http.get("/api/shapemngr/shapetypes").map(this.extractData)
            this.TypesHash = this.Types.map((c: Array<ShapeType>) => {
                    let typeHash: { [typeName: string]: ShapeType } = {};
                    c.forEach((elem) => {
                        typeHash[elem.TypeName] = elem;
                    })
                    return typeHash;
                });
        }
        return this.TypesHash;
    }

    getTypesAsArray(): Observable<Array<ShapeType>> {
        this.getTypes();
        return this.Types;
    }

    getType(name: string): ShapeType {
        if (typeof (this.getTypes()[name]) == "undefined")
            throw "Type " + name + " not found";
        else {
            return this.Types[name];
        }
    }

    getShapes(): Observable<Array<ShapeUI>> {

        return this.getTypes().mergeMap((types:{ [typeName: string]: ShapeType }) => {
            return this.$http.get('api/shapemngr/shapes')
                .map(this.extractData)
                    .map((shapes: Array<ShapeUI>) => {
                        shapes.forEach((elem) => {
                            elem.Type = types[elem.TypeName];
                        });
                        return shapes;
                    });
        });

    }
    createShape(shapeType:string):Observable<ShapeUI>
    {
        return this.$http.put(`/api/shapemngr/createshape?shapeTypeId=${shapeType}`,"")
        .map(this.extractData)
        .mergeMap((sh:ShapeUI)=>
        { 
            return this.getTypes()
            .map(x => {
                sh.Type=x[sh.TypeName];
                return sh;
            })
        });
    }
}

InMemMockService.AddFixture('api_shapesmngr_types', [
    new ShapeType("OrthoMesh", [new ShapeMember("ElemsX", "Elems X", "int"), new ShapeMember("ElemsY", "Elems Y", "int")]),
    new ShapeType("OrthoMesh3D", [new ShapeMember("ElemsX", "Elems X", "int"), new ShapeMember("ElemsY", "Elems Y", "int"), new ShapeMember("ElemsZ", "Elems Z", "int")])
]);

InMemMockService.AddFixture('api_shapesmngr_shapes', [
    new ShapeUI("OrthoMesh", { ElemsX: 1, ElemsY: 2 }, null),
    new ShapeUI("OrthoMesh", { ElemsX: 3, ElemsY: 4 }, null),
    new ShapeUI("OrthoMesh", { ElemsX: 1, ElemsY: 5 }, null),
    new ShapeUI("OrthoMesh3D", { ElemsX: 1, ElemsY: 6,ElemsZ:5 }, null)
]);



