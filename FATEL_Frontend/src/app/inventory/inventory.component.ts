import { Component, OnInit } from '@angular/core';
import {Item} from "../entities/item";
import {ItemService} from "../services/item.service";
import {ITEMS} from "../mock-objects/mock-items";
import {Category} from "../entities/category";

@Component({
  selector: 'inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  items: Item[] = [];
  categories: Category[] = [];

  secondPanelOpenState: boolean = false;

  constructor(private itemService: ItemService) { }

  ngOnInit(): void {
    this.readALlItemsFromInventory();
  }


  private readALlItemsFromInventory(): void {
    this.itemService.readAll()
      .subscribe(items => {
        this.items = items;
        this.categoriseItems();
      });
  }

  private categoriseItems(): void {
    //TODO: Create categorizing logic by name
    /*const names: string[] = [];
    for(const item of this.items){
      if(!names.includes(item.name))
        names.push(item.name);
    }
    console.log(names);*/

    const categories: Category[] = [];
    for(const item of this.items){
      if(!categories.map(value => {
        return value.name
      }).includes(item.name))
        categories.push({name: item.name, items: [item]})
      else {
        // @ts-ignore
        categories.filter(value => {
          return value.name == item.name;
        }).at(0).items.push(item);
      }
    }
    console.log(categories);
  }
}
