import { Component, OnInit } from '@angular/core';

import { Credito } from '../model/app.servicio'

import { ServicioService } from './servicio.service'

@Component({
  selector: 'app-registro',
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css'],
  providers: [ServicioService]  
})
export class RegistroComponent implements OnInit {

  private creditos: Credito[]=[];
  private credito: Credito;
  private errorMessage:string;


  constructor(private service:ServicioService) { }

  ngOnInit() {
    this.credito = new Credito();
    this.getCreditos();
  }

  public getCreditos(){
    this.service.getCreditos().subscribe(
    servicios=>{
      console.log(servicios);
      this.creditos=servicios;
    },
    error=>{
      this.errorMessage=<any>error;
    }
    );
  }

  public pagar(credito:Credito){
    
    console.log("DATOS DEL CREDITO");
    console.log(credito);

    this.service.pagar(credito).subscribe(
    transaccion=>{ 
      console.log(transaccion);
      this.getCreditos(); 
    },
    error=>{this.errorMessage=<any>error;} );
  }

  public extorno(servicio:Credito){
       
    this.service.extorno(servicio).subscribe(
    transaccion=>{ 
      console.log(transaccion);
      this.getCreditos(); 
    },
    error=>{this.errorMessage=<any>error;} );
  }



}
