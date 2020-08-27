import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule }    from '@angular/forms'

import { RegistroRoutingModule } from './registro-routing.module';
import { RegistroComponent } from '../registro/registro.component';


@NgModule({
  declarations: [RegistroComponent],
  imports: [
    CommonModule,
    RegistroRoutingModule,
    FormsModule
  ]
})
export class RegistroModule { }
