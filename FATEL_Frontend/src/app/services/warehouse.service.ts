import {Injectable} from '@angular/core';
import {Warehouse} from '../entities/warehouse'
import axios from "axios";

export const customAxios = axios.create({
  baseURL: 'http://localhost:5175/api/warehouse/',
})

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {
  constructor() {
  }

  async readAll(): Promise<Warehouse[]> {
    const response = await customAxios.get<Warehouse[]>('readall');
    return response.data;
  }
}

