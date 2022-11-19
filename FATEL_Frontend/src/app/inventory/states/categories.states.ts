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
  closed: true,
  editingItem: undefined,
  showRecordMovement: false,
  recordingMovementOnItem: undefined
};

export const reducer = createReducer(
  initialState,
  on(setShowAddItemComponent, state => ({showAddItem: true, showEditItem: false, closed: false, editingItem: undefined, showRecordMovement: false, recordingMovementOnItem: undefined})),
  on(setShowEditItemComponent, (state, {item}) => ({showAddItem: false, showEditItem: true, closed: false, editingItem: item, showRecordMovement: false, recordingMovementOnItem: undefined})),
  on(setShowRecordMovementComponent, (state, {item}) => ({showAddItem: false, showEditItem: false, closed: false, editingItem: undefined, showRecordMovement: true, recordingMovementOnItem: item})),
  on(close, state => ({showAddItem: false, showEditItem: false, closed: true, editingItem: undefined, showRecordMovement: false, recordingMovementOnItem: undefined})),
);

export function CategoriesComponentReducer(state: CategoriesComponentState = initialState, action: Action) {
  return reducer(state, action);
}
