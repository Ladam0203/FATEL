import {Injectable} from '@angular/core';
import {catchError, tap} from 'rxjs/operators';
import {Unit} from "../entities/units";
import {Observable, of} from "rxjs";
import {Item} from "../entities/item";
import {HttpClient} from "@angular/common/http";
import axios from "axios";
import {PostItemDTO} from "../entities/DTOs/PostItemDTO";

export const customAxios = axios.create({
  baseURL: 'http://localhost:5175/api/item/',
})

@Injectable({
  providedIn: 'root'
})
export class ItemService {



  private itemURL = 'http://localhost:5175/api/item/';

  constructor(private http: HttpClient) {

  }

  async readAll(): Promise<Item[]> {
    const response = await customAxios.get<Item[]>('readall');
    return response.data;
    /*return this.http.get<Item[]>(this.itemURL + 'readall')
      .pipe(
        tap(_ => this.log('fetched items')),
        catchError(this.handleError<Item[]>('readAll', []))
      );
     */
  }

  async get(id: any){
    const response = await customAxios.get<any>('item/read/' + id);
    return response.data;
  }

  async create(dto: PostItemDTO) {
    const httpResult = await customAxios.post('create', dto);
    return httpResult.data;
  }

  async delete(id: any){
    const result = await customAxios.delete('item/delete/' + id);
    return result.data;
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  private log(message: string) {
    console.log(`ItemService: ${message}`);
  }
}
