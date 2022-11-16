import {createAction, createReducer, on, Action, props} from "@ngrx/store";
import {Item} from "../../entities/item";

export const setShowAddItemComponent = createAction('[Categories Component] Show Add Item');
export const setShowEditItemComponent = createAction('[Categories Component] Show Edit Item', props<{ item: Item }>());
export const close = createAction('[Categories Component] Close All');


export interface CategoriesComponentState {
  showAddItem: boolean,
  showEditItem: boolean,
  closed: boolean,
  editingItem: Item | undefined,
}

export const initialState: CategoriesComponentState = {
  showAddItem: false,
  showEditItem: false,
  closed: true,
  editingItem: undefined,
};

export const reducer = createReducer(
  initialState,
  on(setShowAddItemComponent, state => ({showAddItem: true, showEditItem: false, closed: false, editingItem: undefined})),
  on(setShowEditItemComponent, (state, {item}) => ({showAddItem: false, showEditItem: true, closed: false, editingItem: item})),
  on(close, state => ({showAddItem: false, showEditItem: false, closed: true, editingItem: undefined})),
);

export function CategoriesComponentReducer(state: CategoriesComponentState = initialState, action: Action) {
  return reducer(state, action);
}

export const selectAddItem = (state: CategoriesComponentState) => state.showAddItem;
export const selectEditItem = (state: CategoriesComponentState) => state.showEditItem;
export const selectClosed = (state: CategoriesComponentState) => state.closed;
