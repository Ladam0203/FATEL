import { Injectable } from '@angular/core';
import {Login} from "../entities/DTOs/login";
import axios from "axios";
import {environment} from "../../environments/environment";
import {MatSnackBar} from "@angular/material/snack-bar";
import {catchError} from "rxjs/operators";

export const customAxios = axios.create({
  baseURL: environment.baseUrl
})

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private matSnackBar: MatSnackBar) {
    customAxios.interceptors.response.use(
      response => {
        return response;
      },
      rejected =>{
        if(!rejected.response)
        {
          this.matSnackBar.open("Could not connect to server",
            undefined,
            {duration: 4000});
        }
        else if(rejected.response.status == 401){
          this.matSnackBar.open("Incorrect username or password",
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
