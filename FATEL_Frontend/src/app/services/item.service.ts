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
import {TranslateService} from "@ngx-translate/core";
import {PatchItemNameDTO} from "../entities/DTOs/PatchItemNameDTO";

export const customAxios = axios.create({
  baseURL: environment.baseUrl,
})

@Injectable({
  providedIn: 'root'
})
export class ItemService {

  constructor(private http: HttpClient,
              private matSnackBar: MatSnackBar,
              private router: Router,
              private translate: TranslateService) {
    customAxios.interceptors.response.use(
      response => {
        if (response.status == 201) {
          this.matSnackBar.open(translate.instant("API-SERVICE.SNACKBAR.ITEM-CREATED"),
            undefined,
            {duration: 4000});
        }
        return response;
      },
      rejected => {
        if(!rejected.response)
        {
          this.matSnackBar.open(translate.instant("API-SERVICE.SNACKBAR.NO-CONNECTION"),
            undefined,
            {duration: 4000});
        }
        else if (rejected.response.status == 401) {

          this.matSnackBar.open(translate.instant("API-SERVICE.SNACKBAR.INVALID-TOKEN"),

            undefined,
            {duration: 4000});
          this.router.navigate(['./login'])
        }
        else if (rejected.response.status == 403) {

          this.matSnackBar.open(translate.instant("API-SERVICE.SNACKBAR.SAME-ITEM-EXISTS"),
            undefined,
            {duration: 4000});
        } else if (rejected.response.status == 500) {
          this.matSnackBar.open("API-SERVICE.SNACKBAR.INTERNAL-SERVER-ERROR",
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

  async updateNameRange(dtos: PatchItemNameDTO[]) {
    const response = await customAxios.patch('item/updateNameRange', dtos, {
      headers: {
        Authorization:`Bearer ${localStorage.getItem('token')}`
      }
    });
    return response.data;
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
