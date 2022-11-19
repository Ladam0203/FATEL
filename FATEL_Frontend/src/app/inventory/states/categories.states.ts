import {createAction, createReducer, on, Action, props} from "@ngrx/store";
import {Item} from "../../entities/item";

export const setShowAddItemComponent = createAction('[Categories Component] Show Add Item');
export const setShowEditItemComponent = createAction('[Categories Component] Show Edit Item', props<{ item: Item }>());
export const setShowRecordMovementComponent = createAction('[Categories Component] Show Record Movement', props<{ item: Item }>());
export const close = createAction('[Categories Component] Close All');


export interface CategoriesComponentState {
  showAddItem: boolean,
  showEditItem: boolean,
  showRecordMovement: boolean,
  closed: boolean,
  editingItem: Item | undefined,
  recordingMovementOnItem: Item | undefined
}

export const initialState: CategoriesComponentState = {
  showAddItem: false,
  showEditItem: false,
  showRecordMovement: false,
  closed: true,
  editingItem: undefined,
  recordingMovementOnItem: undefined
};

export const reducer = createReducer(
  initialState,
  on(setShowAddItemComponent, state => ({...initialState, showAddItem: true, closed: false})),
  on(setShowEditItemComponent, (state, {item}) => ({...initialState, showEditItem: true, editingItem: item, closed: false, })),
  on(setShowRecordMovementComponent, (state, {item}) => ({...initialState, showRecordMovement: true, recordingMovementOnItem: item, closed: false, })),
  on(close, state => ({...initialState}))
);

export function CategoriesComponentReducer(state: CategoriesComponentState = initialState, action: Action) {
  return reducer(state, action);
}
