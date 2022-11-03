import {Warehouse} from '../entities/warehouse'
import {ITEMS} from './mock-items'
import {ENTRIES} from './mock-entries'

export const WAREHOUSES: Warehouse[] = [
  {Id: 1, Name: 'Warehouse #1', Inventory: ITEMS, Diary: ENTRIES},
  {Id: 2, Name: 'Warehouse #2', Inventory: [], Diary: []},
  {Id: 3, Name: 'Warehouse #3', Inventory: ITEMS, Diary: ENTRIES},
  {Id: 4, Name: 'Warehouse #4', Inventory: [], Diary: []},
  {Id: 5, Name: 'Warehouse #5', Inventory: ITEMS, Diary: ENTRIES},
]
