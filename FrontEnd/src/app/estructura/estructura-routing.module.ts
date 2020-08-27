import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EstructuraComponent } from './estructura.component';

const routes: Routes = [     
  { 
    path: '', 
    redirectTo: 'estructura', 
    pathMatch: 'full'
  },      
  { 
    path: 'estructura', 
    component: EstructuraComponent,
    children: [ 
      {
        path: 'minicio', 
        loadChildren: '../inicio/inicio.module#InicioModule'
      },
      {
        path: 'mregistro', 
        loadChildren: '../registro/registro.module#RegistroModule'
      }
    ] 

  } 
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EstructuraRoutingModule { }
