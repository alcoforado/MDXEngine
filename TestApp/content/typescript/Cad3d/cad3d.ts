import { NgModule } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import {Component} from '@angular/core';
import { InMemoryWebApiModule } from 'angular-in-memory-web-api';


//helloffsdfdgsffsfs
@Component({
    selector: 'my-app',
    template:`<p>Hello World</p>`
})
export class AppComponent {
    
}















@NgModule({
    imports: [
        BrowserModule,
        HttpModule
    ],
    declarations: [
        BrowserModule
    ],
    providers: [
    ],
    bootstrap: [AppComponent]
})
class AppModule { }

platformBrowserDynamic().bootstrapModule(AppModule)

alert("Iam here");