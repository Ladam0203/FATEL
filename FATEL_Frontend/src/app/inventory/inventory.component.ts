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
    this.itemService.readAll()
      .subscribe(items => {
        this.items = items;
        this.categoriseItems();
      });
  }

  private categoriseItems(): void {
    const categories: Category[] = [];
    for(const item of this.items){
      //if the item's name is not in the category array
      if(!categories.map(value => value.name).includes(item.name))
        //add new category with the item's name & the item itself
        categories.push({name: item.name, items: [item]})
      else {
        //if the item's name is in the category array, get the first occurrence and add the current item
        const category = categories.filter(value => value.name == item.name).at(0);
        if(category)
          category.items.push(item);
      }
    }
  }
}
