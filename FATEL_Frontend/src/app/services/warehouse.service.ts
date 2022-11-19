import {Injectable} from '@angular/core';
import {Warehouse} from '../entities/warehouse'
import {Observable, of} from 'rxjs';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {catchError, map, tap} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {
  private warehouseUrl = 'api/warehouse'; //URL to web api

  httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  }

  constructor(private http: HttpClient) {

  }
  /*
  getAllWarehouses(): Observable<Warehouse[]> {
    return this.http.get<Warehouse[]>(this.warehouseUrl)
      .pipe(
        tap(_ => this.log('fetched warehouses')),
        catchError(this.handleError<Warehouse[]>('getAllWarehouses', []))
      );
  }*/

  private handleError<T>(operation = 'operation', result?: T){
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    }
  }

  getWarehouse(id: number): Observable<Warehouse> {
    const url = `${this.warehouseUrl}/${id}`;
    return this.http.get<Warehouse>(url).pipe(
      tap(_ => this.log(`fetched warehouse id=${id}`)),
      catchError(this.handleError<Warehouse>(`getWarehouse id=${id}`))
    )
  }

  private log(message: string){
    console.log(message);
  }
}
