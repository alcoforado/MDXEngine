import { NgModule } from '@angular/core';
import {ReactiveFormsModule } from '@angular/forms'
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { BrowserModule } from '@angular/platform-browser';
import {Component} from '@angular/core';
import { InMemoryWebApiModule } from 'angular-in-memory-web-api';
import {InMemMockService} from './services/mocks'
import {HttpModule} from '@angular/http';
import {ShapesMngrService} from './services/shapes-mngr-service';
import {ShapesMngrComponent} from './shapes-mngr/shapes-mngr.component';
import {AppComponent} from './app.component';
import {MFormModule} from './modules/mform/mform.module'

@NgModule({
    imports: [
        HttpModule,
        BrowserModule,
        ReactiveFormsModule,
        MFormModule,
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
export class AppModule { }