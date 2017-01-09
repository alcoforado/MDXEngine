import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import {NumberInputComponent} from './number-input/number-input';
import { DynamicInputComponent } from './dynamic-input/dynamic-input.component'


@NgModule({
  declarations: [
      NumberInputComponent
     
  ],
  imports: [
   ReactiveFormsModule
  ],
  exports: [
    NumberInputComponent
  ],
  providers: []
})
export class MFormModule { }
