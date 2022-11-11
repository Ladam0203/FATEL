import {AfterViewInit, Component, OnInit} from '@angular/core';
import {Item} from "../entities/item";
import {ItemService} from "../services/item.service";
import {Category} from "../entities/category";
import {Unit} from "../entities/units";

@Component({
  selector: 'inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'width', 'length', 'unit', 'quantity', 'note', 'actions'];

  units: typeof Unit = Unit;
  categories: Category[] = [];
  items: Item[] = [];

  query: string = '';

  showAddItemComponent: boolean = false;

  constructor(private itemService: ItemService) {
  }

  ngOnInit(): void {
    this.itemService.readAll()
      .then(items => {
        this.items = items;
        this.categoriseItems();
      });
  }

  addItem(newItem: Item) {
    this.items.push(newItem);
    this.categoriseItems();
  }

  private categoriseItems(): void {
    this.categories = [];
    for (const item of this.items) {
      let category = this.categories.find(c => c.name === item.name && c.unit === item.unit);
      if (category == null) {
        category = new Category(item.name, item.unit);
        this.categories.push(category);
      }
      category.items.push(item);
    }
  }

  buttonClick() {
    alert('here');
  }

  openAddItemComponent() {
    this.showAddItemComponent = true;
  }

  closeAddItemComponent() {
    this.showAddItemComponent = false;
  }

  rowClick(row: any) {
    alert(row.id);
    console.log(row)
  }
}
