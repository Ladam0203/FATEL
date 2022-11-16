import {Injectable} from '@angular/core';
import {catchError} from 'rxjs/operators';
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
        if (response.status == 201) {
          this.matSnackBar.open("Item Created",
            undefined,
            {duration: 4000});
        }
        return response;
      },
      rejected => {
        if (rejected.response.status >= 400 && rejected.response.status < 500) {
          this.matSnackBar.open(rejected.response.data,
            undefined,
            {duration: 4000});
        } else if (rejected.response.status > 499) {
          this.matSnackBar.open("Something went wrong",
            undefined,
            {duration: 4000})
        }
        catchError(rejected);
      }
    )
  }

  async create(dto: PostItemDTO) {
    const response = await customAxios.post('create', dto);
    return response.data;
  }

  async readAll(): Promise<Item[]> {
    const response = await customAxios.get<Item[]>('readall');
    return response.data;
  }

  async update(item: any) {
    const response = await customAxios.put<any>('update/' + item.id, item);
    return response.data;
  }

  async delete(id: any) {
    const response = await customAxios.delete('delete/' + id);
    return response.data;
  }

  async get(id: any) {
    const response = await customAxios.get<any>('read/' + id);
    return response.data;
  }
}
