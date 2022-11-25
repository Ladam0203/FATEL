import {Injectable} from '@angular/core';
import {Warehouse} from '../entities/warehouse'
import axios from "axios";
import {PostWarehouseDTO} from "../entities/DTOs/PostWarehouseDTO";
import {PutWarehouseDTO} from "../entities/DTOs/PutWarehouseDTO";

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

  async create(dto: PostWarehouseDTO): Promise<Warehouse> {
    console.log("called")
    const response = await customAxios.post<Warehouse>('create', dto);
    return response.data;
  }

  async update(dto: PutWarehouseDTO): Promise<Warehouse> {
    const response = await customAxios.put<Warehouse>('update', dto);
    return response.data;
  }

  async delete(id: number): Promise<void> {
    const response = await customAxios.delete(`delete/${id}`);
    return response.data;
  }
}

