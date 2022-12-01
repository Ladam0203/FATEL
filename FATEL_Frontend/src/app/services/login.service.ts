import { Injectable } from '@angular/core';
import {Login} from "../entities/DTOs/login";
import axios from "axios";
import {environment} from "../../environments/environment";

export const customAxios = axios.create({
  baseURL: environment.baseUrl
})

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor() { }

  async login(dto: Login){
    const result = await customAxios.post('login', dto);
    return result.data;
  }

}
