import { NgModule } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { BrowserModule } from '@angular/platform-browser';
import {Component} from '@angular/core';
import { InMemoryWebApiModule } from 'angular-in-memory-web-api';
import {InMemMockService} from '../services/mocks'
import {HttpModule} from '@angular/http';
import {ShapesMngrService} from '../services/shapes-mngr-service';
import {ShapesMngrComponent} from 'typescript/Cad3d/shapes-mngr.component';

@Component({
    moduleId: module.id,
    selector: 'my-app',
    templateUrl: 'app.component.html'
})
export class AppComponent {
   
}
















@NgModule({
    imports: [
        HttpModule,
        BrowserModule,
        InMemoryWebApiModule.forRoot(InMemMockService)
    ],
    declarations: [
        AppComponent,
        ShapesMngrComponent
    ],
    providers: [
        ShapesMngrService
    ],
    bootstrap: [AppComponent]
})
class AppModule { }

platformBrowserDynamic().bootstrapModule(AppModule)

