import {Injectable} from '@angular/core';
import {Warehouse} from '../entities/warehouse'
import axios from "axios";
import {PostWarehouseDTO} from "../entities/DTOs/PostWarehouseDTO";
import {PutWarehouseDTO} from "../entities/DTOs/PutWarehouseDTO";
import {environment} from "../../environments/environment";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";
import {catchError} from "rxjs/operators";
import {TranslateService} from "@ngx-translate/core";

export const customAxios = axios.create({
  baseURL: environment.baseUrl,
})

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {
  constructor(private matSnackBar: MatSnackBar,
              private router: Router,
              private translate: TranslateService) {
    customAxios.interceptors.response.use(
      response => {
        if (response.status == 201) {
          this.matSnackBar.open(translate.instant("API-SERVICE.SNACKBAR.WAREHOUSE-CREATED"),
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
        else if (rejected.response.status ==401) {
          this.matSnackBar.open(translate.instant("API-SERVICE.SNACKBAR.INVALID-TOKEN"),
            undefined,
            {duration: 4000});
          this.router.navigate(['./login'])
        }
        catchError(rejected);
      }
    )
  }

  async readAll(): Promise<Warehouse[]> {
    const response = await customAxios.get<Warehouse[]>('warehouse/readall', {
      headers: {
        Authorization:`Bearer ${localStorage.getItem('token')}`
      }
    });
    return response.data;
  }

  async create(dto: PostWarehouseDTO): Promise<Warehouse> {
    const response = await customAxios.post<Warehouse>('warehouse/create', dto, {
      headers: {
        Authorization:`Bearer ${localStorage.getItem('token')}`
      }
    });
    return response.data;
  }

  async update(dto: PutWarehouseDTO): Promise<Warehouse> {
    const response = await customAxios.put<Warehouse>(`warehouse/update/${dto.id}`, dto, {
      headers: {
        Authorization:`Bearer ${localStorage.getItem('token')}`
      }
    });
    return response.data;
  }

  async delete(id: number): Promise<Warehouse> {
    const response = await customAxios.delete(`warehouse/delete/${id}`, {
      headers: {
        Authorization:`Bearer ${localStorage.getItem('token')}`
      }
    });
    return response.data;
  }
}

