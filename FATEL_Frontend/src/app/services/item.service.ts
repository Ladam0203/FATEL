import {Injectable} from '@angular/core';
import axios from "axios";
import {MatSnackBar} from '@angular/material/snack-bar';
import {catchError} from 'rxjs/operators';
import {Unit} from "../entities/units";


export const customAxios = axios.create({
  baseURL: 'http://localhost:5175/api',
})


@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(private matSnackbar: MatSnackBar) {
    customAxios.interceptors.response.use(
      response => {
        if (response.status == 201) {
          this.matSnackbar.open("Great success")
        }
        return response;
      }, rejected => {
        if (rejected.response.status >= 400 && rejected.response.status < 500) {
          matSnackbar.open(rejected.response.data);
        } else if (rejected.response.status > 499) {
          this.matSnackbar.open("Something went wrong")
        }
        catchError(rejected);
      }
    )
  }

  async getAll() {
    const response = await customAxios.get<any>('item/readall');
    return response.data;
  }

  async get(id: any){
    const response = await customAxios.get<any>('item/read/' + id);
    return response.data;
  }

  async create(dto: {
    Name: string, Width?: number, Length?: number,
    Unit: Unit, Quantity: number, Note?: string
  }) {
    const httpResult = await customAxios.post('item/create', dto);
    return httpResult.data;
  }

  async delete(id: any){
    const result = await customAxios.delete('item/delete/' + id);
    return result.data;
  }
}
