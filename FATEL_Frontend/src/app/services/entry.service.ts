import {Injectable} from '@angular/core';
import {catchError} from 'rxjs/operators';
import {Item} from "../entities/item";
import {HttpClient} from "@angular/common/http";
import axios from "axios";
import {PostItemDTO} from "../entities/DTOs/PostItemDTO";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Movement} from "../entities/DTOs/Movement";
import {Entry} from "../entities/entry";

export const customAxios = axios.create({
  baseURL: 'http://localhost:5175/api/entry/',
})

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(private http: HttpClient,
              private matSnackBar: MatSnackBar) {
    /*
    customAxios.interceptors.response.use(
      response => {
        if (response.status == 201) {
          this.matSnackBar.open("Entry Created",
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
          this.matSnackBar.open(JSON.stringify(rejected.response),
            undefined,
            {duration: 4000})
        }
        catchError(rejected);
      }
    )
     */
  }

  async readAll(): Promise<Entry[]> {
    const response = await customAxios.get<Entry[]>('readall');
    return response.data;
  }
}
