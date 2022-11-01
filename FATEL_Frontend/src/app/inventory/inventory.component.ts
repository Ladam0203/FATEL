import { Component, OnInit } from '@angular/core';
import {Item} from "../entities/item";
import {ItemService} from "../services/item.service";
//import {ITEMS} from "../mock-objects/mock-items";

@Component({
  selector: 'inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  items: any[] = [];

  constructor(private itemService: ItemService) { }

  async ngOnInit() {
    const items = await this.itemService.getAll();
    this.items = items;
    //this.getAllItems();
  }
}
