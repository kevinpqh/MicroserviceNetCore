import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EstructuraRoutingModule } from './estructura-routing.module';
import { EstructuraComponent } from '../estructura/estructura.component';
import { MenuComponent } from '../estructura/menu/menu.component';
import { FooterComponent } from '../estructura/footer/footer.component';
import { HeaderComponent } from '../estructura/header/header.component';


@NgModule({
  declarations: [EstructuraComponent, MenuComponent, FooterComponent, HeaderComponent],
  imports: [
    CommonModule,
    EstructuraRoutingModule
  ]
})
export class EstructuraModule { }
