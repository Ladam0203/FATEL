import {createAction, createReducer, on, Action, props} from "@ngrx/store";
import {Item} from "../../entities/item";
import {Warehouse} from "../../entities/warehouse";
import {Entry} from "../../entities/entry";

export const setShowAddItemComponent = createAction('[Categories Component] Show Add Item');
export const setShowEditItemComponent = createAction('[Categories Component] Show Edit Item', props<{ item: Item }>());
export const setShowRecordMovementComponent = createAction('[Categories Component] Show Record Movement', props<{ item: Item }>());
export const close = createAction('[Categories Component] Close All');

export const setSelectedWarehouse = createAction('[Sidenav Component] Set Warehouse', props<{ warehouse: Warehouse }>());

export const addItemAction = createAction('[Add Item Component] Add Item', props<{ item: Item }>());
export const editItemAction = createAction('[Edit Item Component] Edit Item', props<{ item: Item }>());
export const deleteItemAction = createAction('[Categories Component] Delete Item', props<{ item: Item }>());

export const addEntryAction = createAction('[Add Item Component] Add Entry', props<{ entry: Entry }>());

export const editWarehouseAction = createAction('[Tool Bar Component] Edit Warehouse', props<{ warehouse: Warehouse }>());

export interface AppState {
  showAddItem: boolean,
  showEditItem: boolean,
  showRecordMovement: boolean,
  closed: boolean,

  selectedItem: Item | undefined,

  selectedWarehouse: Warehouse,
}

export const initialState: AppState = {
  showAddItem: false,
  showEditItem: false,
  showRecordMovement: false,
  closed: true,

  selectedItem: undefined,
  selectedWarehouse: {id: 0, name: '', diary: [], inventory: []},
};

export const reducer = createReducer(
  initialState,

  on(setShowAddItemComponent, state => ({
    ...initialState,
    showAddItem: true,
    closed: false,
    selectedWarehouse: state.selectedWarehouse
  })),

  on(setShowEditItemComponent, (state, {item}) => ({
    ...initialState,
    showEditItem: true,
    closed: false,

    selectedItem: item,
    selectedWarehouse: state.selectedWarehouse
  })),

  on(setShowRecordMovementComponent, (state, {item}) => ({
    ...initialState,
    showRecordMovement: true,
    closed: false,

    selectedItem: item,
    selectedWarehouse: state.selectedWarehouse
  })),

  on(close, state => ({
    ...initialState,
    selectedWarehouse: state.selectedWarehouse

  })),

  on(setSelectedWarehouse, (state, {warehouse}) => ({
    ...initialState,
    selectedWarehouse: warehouse,
  })),

  on(addItemAction, (state, {item}) => ({
    ...state,
    selectedWarehouse: {
      ...state.selectedWarehouse,
      inventory: [
        ...state.selectedWarehouse.inventory,
        item
      ]
    }
  })),

  on(editItemAction, (state, {item}) => ({
    ...state,
    selectedWarehouse: {
      ...state.selectedWarehouse,
      inventory: [
        ...state.selectedWarehouse.inventory.filter(items => items.id != item.id),
        item
      ]
    }
  })),

  on(deleteItemAction, (state, {item}) => ({
    ...state,
    selectedWarehouse: {
      ...state.selectedWarehouse,
      inventory: [
        ...state.selectedWarehouse.inventory.filter(items => items.id != item.id)
      ]
    }
  })),

  on(addEntryAction, (state, {entry}) => ({
    ...state,
    selectedWarehouse: {
      ...state.selectedWarehouse,
      diary: [
        ...state.selectedWarehouse.diary,
        entry
      ]
    }
  })),

  on(editWarehouseAction, (state, {warehouse}) => ({
    ...state,
    selectedWarehouse: {
      ...state.selectedWarehouse,
      name: warehouse.name
    }
  })),
);

export function AppReducer(state: AppState = initialState, action: Action) {
  return reducer(state, action);
}
