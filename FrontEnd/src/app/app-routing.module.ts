import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AutenticacionGuardService } from './seguridad/autenticacion-guard.service';

export const routes: Routes = [
  { path: '', redirectTo: 'mlogin', pathMatch: 'full'},
  { path: 'mlogin', loadChildren: () => import('./login/login.module').then(m => m.LoginModule) },  
  { path: 'mestructura', loadChildren: () => import('./estructura/estructura.module').then(m => m.EstructuraModule), canActivate:[AutenticacionGuardService]}, 
  { path: '**', redirectTo: '' } // otherwise redirect to home 
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
