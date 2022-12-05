import {Injectable} from '@angular/core';
import {catchError} from 'rxjs/operators';
import {Item} from "../entities/item";
import {HttpClient} from "@angular/common/http";
import axios from "axios";
import {PostItemDTO} from "../entities/DTOs/PostItemDTO";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Movement} from "../entities/DTOs/Movement";
import {environment} from "../../environments/environment";
import {Router} from "@angular/router";

export const customAxios = axios.create({
  baseURL: environment.baseUrl,
})

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(private http: HttpClient,
              private matSnackBar: MatSnackBar,
              private router: Router) {
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
        if(!rejected.response)
        {
          this.matSnackBar.open("Could not connect to server",
            undefined,
            {duration: 4000});
        }
        else if (rejected.response.status ==401) {
          this.matSnackBar.open("Please login to continue",
            undefined,
            {duration: 4000});
          this.router.navigate(['./login'])
        }
        else if (rejected.response.status >= 400 && rejected.response.status < 500) {
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
  }

  async create(dto: PostItemDTO) {
    const response = await customAxios.post('item/create', dto, {
      headers: {
        Authorization:`Bearer ${localStorage.getItem('token')}`
      }
    });
    return this.mapResponse(response.data);
  }

  async update(item: any) {
    const response = await customAxios.put<any>('item/update/' + item.id, item, {
      headers: {
        Authorization:`Bearer ${localStorage.getItem('token')}`
      }
    });
    return this.mapResponse(response.data);
  }

  async updateQuantity(movement: Movement) {
    const response = await customAxios.put<Item>('item/updateQuantity/' + movement.item.id, movement,{
      headers: {
        Authorization:`Bearer ${localStorage.getItem('token')}`
      }
    });
    return this.mapResponse(response.data);
  }

  async delete(id: any) {
    const response = await customAxios.delete('item/delete/' + id, {
      headers: {
        Authorization:`Bearer ${localStorage.getItem('token')}`
      }
    });
    return this.mapResponse(response.data);
  }

  private mapResponse(data: any) {
    return {
      item: {
        id: data.id,
        name: data.name,
        length: data.length,
        width: data.width,
        unit: data.unit,
        quantity: data.quantity,
        note: data.note,
      },
      entry: data.entry
    };
  }
}
