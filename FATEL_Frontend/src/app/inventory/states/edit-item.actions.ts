import {createAction, props, createReducer, on, Action, createFeatureSelector, createSelector} from "@ngrx/store";
import {Item} from "../../entities/item";

export const setShowEditItemComponent = createAction('[Edit-Item Component] Set', props<{value: boolean, payload?: Item}>());

export interface EditItemComponentState {
  value: boolean;
  editingItem: Item | undefined;
}

export const initialState: EditItemComponentState = {
  value: false,
  editingItem: undefined,
};

export const reducer = createReducer(
  initialState,
  on(setShowEditItemComponent, (state, {value, payload}) => ({...state, value, editingItem: payload})),
);

export function showEditItemComponentReducer(state: EditItemComponentState = initialState, action: Action){
  return reducer(state, action);
}

export const selectShowEditItemComponentFeature = createFeatureSelector<EditItemComponentState>('showEditItemComponent');

export const selectShowEditItemComponentValue = createSelector(
  selectShowEditItemComponentFeature,
  state => state.value,
);

export const selectEditItemComponentItem = createSelector(
  selectShowEditItemComponentFeature,
  state => state.editingItem,
);
