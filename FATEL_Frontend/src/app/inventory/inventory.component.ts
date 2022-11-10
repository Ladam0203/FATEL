  import {Component, OnInit} from '@angular/core';
import {Item} from "../entities/item";
import {ItemService} from "../services/item.service";
import {ITEMS} from "../mock-objects/mock-items";
import {Category} from "../entities/category";
import {Unit, UnitUtil} from "../entities/units";

@Component({
  selector: 'inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'width', 'length', 'unit', 'quantity', 'note'];

  units: typeof Unit = Unit;
  categories: Category[] = [];
  items: Item[] = [];

  constructor(private itemService: ItemService) {
  }

  ngOnInit(): void {
    this.itemService.readAll()
      .then(items =>{
        this.items = items;
        this.categoriseItems(this.items);
        this.buildDescriptions(this.categories);
      });
  }

  addItem(newItem: Item) {
    this.items.push(newItem);
    this.categoriseItems(this.items);
    this.buildDescriptions(this.categories);
  }

  private categoriseItems(items: Item[]): void {
    this.categories = [];
    for (const item of items) {
      //if the item's name is not in the categories array
      if (!this.categories.map(value => value.name).includes(item.name))
        //add new category with the item's name & the item itself
        this.categories.push({name: item.name, items: [item], description: ""})
      else {
        //if the item's name is in the categories array, get the first occurrence and add the current item
        const category = this.categories.filter(value => value.name == item.name).at(0);
        if (category)
          category.items.push(item);
      }
    }
  }

  private buildDescriptions(categories: Category[]) {
    for (const category of categories) {
      let total = 0;
      let suffix = category.items[0].unit;
      for (const item of category.items) {
        total += item.quantity;
      }
      category.description = "" + total + UnitUtil.abbreviations(suffix);
    }
  }

  buttonClick() {
    alert('here');
  }
}
