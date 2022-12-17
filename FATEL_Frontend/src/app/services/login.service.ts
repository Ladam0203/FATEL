import { Injectable } from '@angular/core';
import {Login} from "../entities/DTOs/login";
import axios from "axios";
import {environment} from "../../environments/environment";
import {MatSnackBar} from "@angular/material/snack-bar";
import {catchError} from "rxjs/operators";
import { Router } from '@angular/router';
import {TranslateService} from "@ngx-translate/core";

export const customAxios = axios.create({
  baseURL: environment.baseUrl
})

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private matSnackBar: MatSnackBar,
              private translate: TranslateService,) {
    customAxios.interceptors.response.use(
      response => {
        return response;
      },
      rejected =>{
        if(!rejected.response)
        {
          this.matSnackBar.open(translate.instant("API-SERVICE.SNACKBAR.NO-CONNECTION"),
            undefined,
            {duration: 4000});
        }
        else if(rejected.response.status == 401){
          this.matSnackBar.open(translate.instant("API-SERVICE.SNACKBAR.INCORRECT-CREDENTIALS"),
            undefined,
            {duration: 4000});
        }
        catchError(rejected);
      }
    )
  }

  async login(dto: Login){
    try {
      const result = await customAxios.post('user/login', dto);
      return result.data;
    }
    catch (error){
      return false;
    }
  }
}
