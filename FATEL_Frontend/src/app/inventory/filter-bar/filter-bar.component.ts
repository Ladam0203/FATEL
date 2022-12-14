import {Component, OnInit} from '@angular/core';
import {Store} from "@ngrx/store";
import {selectSearchbarQueryValue, setSearchbarQuery} from "../../states/filter-bar.actions";

@Component({
  selector: 'app-filter-bar',
  templateUrl: './filter-bar.component.html',
  styleUrls: ['./filter-bar.component.css']
})
export class FilterBarComponent implements OnInit {

  searchbarQuery = this.store.select(selectSearchbarQueryValue);
  query: string = '';

  constructor(private readonly store: Store<any>) {
  }

  ngOnInit(): void {
    this.searchbarQuery.subscribe(value => this.query = value);
  }

  queryChange() {
    this.store.dispatch(setSearchbarQuery({value: this.query}));
  }
}
