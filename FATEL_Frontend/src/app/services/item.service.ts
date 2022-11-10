import {Injectable} from '@angular/core';
import {catchError, tap} from 'rxjs/operators';
import {Unit} from "../entities/units";
import {Observable, of} from "rxjs";
import {Item} from "../entities/item";
import {HttpClient} from "@angular/common/http";
import axios from "axios";
import {PostItemDTO} from "../entities/DTOs/PostItemDTO";
import {MatSnackBar} from "@angular/material/snack-bar";

export const customAxios = axios.create({
  baseURL: 'http://localhost:5175/api/item/',
})

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(private http: HttpClient,
              private matSnackBar: MatSnackBar) {
    customAxios.interceptors.response.use(
      response => {
        if(response.status==201){
          this.matSnackBar.open("Item Created",
            undefined,
            {duration: 4000});
        }
        return response;
      },
      rejected =>{
        if(rejected.response.status>=400 && rejected.response.status < 500) {
          this.matSnackBar.open(rejected.response.data,
            undefined,
            {duration: 4000});
        }
        else if(rejected.response.status > 499){
          this.matSnackBar.open("Something went wrong",
            undefined,
            {duration: 4000})
        }
        catchError(rejected);
      }
    )
  }

  async readAll(): Promise<Item[]> {
    const response = await customAxios.get<Item[]>('readall');
    return response.data;
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
