import { Component, OnInit } from '@angular/core';
import {Item} from "../entities/item";
import {ItemService} from "../services/item.service";
import {ITEMS} from "../mock-objects/mock-items";

@Component({
  selector: 'inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  items: Item[] = [];

  constructor(private itemService: ItemService) { }

  ngOnInit(): void {
    this.getAllItemsFromInventory();
  }


  private getAllItemsFromInventory(): void {
    this.itemService.getAll()
      .subscribe(items => {
        this.items = items;
        console.log(items);
      });
  }
}
