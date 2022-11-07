import { Component, OnInit } from '@angular/core';
import {Item} from "../entities/item";
import {ItemService} from "../services/item.service";
import {ITEMS} from "../mock-objects/mock-items";
import {Category} from "../entities/category";
import {Unit} from "../entities/units";

@Component({
  selector: 'inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'width', 'length', 'unit', 'quantity', 'note'];

  units: typeof Unit = Unit;
  items: Item[] = [];
  categories: Category[] = [];

  constructor(private itemService: ItemService) { }

  ngOnInit(): void {
    this.itemService.readAll()
      .subscribe(items => {
        this.items = items;
        this.categoriseItems();
      });
      console.log(this.units.keys());
  }

  private categoriseItems(): void {
    for(const item of this.items){
      //if the item's name is not in the categories array
      if(!this.categories.map(value => value.name).includes(item.name))
        //add new category with the item's name & the item itself
        this.categories.push({name: item.name, items: [item]})
      else {
        //if the item's name is in the categories array, get the first occurrence and add the current item
        const category = this.categories.filter(value => value.name == item.name).at(0);
        if(category)
          category.items.push(item);
      }
    }
  }
}
