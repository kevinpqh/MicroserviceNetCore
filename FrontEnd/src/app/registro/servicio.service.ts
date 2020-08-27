import { Injectable } from '@angular/core';

import { HttpHeaders, HttpResponse } from '@angular/common/http';

import { HttpClient } from '@angular/common/http';
import { Credito } from '../model/app.servicio';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Transaccion } from '../model/transaccion';

const httpOptions = {
  headers: new HttpHeaders(
    { 
      'Content-Type': 'application/json',
      'Authorization': localStorage.getItem('token') 
    }
  )
};


@Injectable({
  providedIn: 'root'
})
export class ServicioService {

  public servicioUrl = 'https://localhost:5000/api/credit';  // URL to web API  
  public creditos: Credito[] = [];  
  public credito: Credito; 
  public  errorMessage: string; 


  constructor(private http: HttpClient) { }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
    console.error(error); // log to console instead
    return of(result as T);
    };  
  }

  public getCreditos(): Observable<Credito[]> {
    return this.http.get<Credito[]>(this.servicioUrl+"", httpOptions).pipe(
    catchError(this.handleError('getCreditos', [])) );
  }

  public pagar(servicio: Credito): Observable<Transaccion> { 
    return this.http.post<Transaccion>(this.servicioUrl+"/pay",JSON.stringify(servicio), httpOptions).pipe(
    tap((transaccion: Transaccion) => console.log(transaccion.codigo)),
    catchError(this.handleError<Transaccion>('update'))
    ); 
  }

  public extorno(servicio: Credito): Observable<Transaccion> { 
    return this.http.post<Transaccion>(this.servicioUrl+"/reverse",JSON.stringify(servicio), httpOptions).pipe(
    tap((transaccion: Transaccion) => console.log(transaccion.codigo)),
    catchError(this.handleError<Transaccion>('reverse'))
    ); 
  }



}
