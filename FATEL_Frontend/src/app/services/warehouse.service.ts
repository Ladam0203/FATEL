import {Injectable} from '@angular/core';
import {Warehouse} from '../entities/warehouse'
import axios from "axios";
import {PostWarehouseDTO} from "../entities/DTOs/PostWarehouseDTO";
import {PutWarehouseDTO} from "../entities/DTOs/PutWarehouseDTO";
import {environment} from "../../environments/environment";

export const customAxios = axios.create({
  baseURL: environment.baseUrl,
  headers: {
    Authorization:`Bearer ${localStorage.getItem('token')}`
  }
})

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {
  constructor() {
  }

  async readAll(): Promise<Warehouse[]> {
    const response = await customAxios.get<Warehouse[]>('warehouse/readall');
    return response.data;
  }

  async create(dto: PostWarehouseDTO): Promise<Warehouse> {
    const response = await customAxios.post<Warehouse>('warehouse/create', dto);
    return response.data;
  }

  async update(dto: PutWarehouseDTO): Promise<Warehouse> {
    const response = await customAxios.put<Warehouse>(`warehouse/update/${dto.id}`, dto);
    return response.data;
  }

  async delete(id: number): Promise<Warehouse> {
    const response = await customAxios.delete(`warehouse/delete/${id}`);
    return response.data;
  }
}

