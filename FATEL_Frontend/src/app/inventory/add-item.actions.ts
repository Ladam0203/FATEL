import {createAction, props, createReducer, on, Action, createFeatureSelector, createSelector} from "@ngrx/store";

export const setShowAddItemComponent = createAction('[Add-Item Component] Set', props<{value: boolean}>());

export interface AddItemComponentState {
  value: boolean;
}

export const initialState: AddItemComponentState = {
  value: false,
};

export const reducer = createReducer(
  initialState,
  on(setShowAddItemComponent, (state, {value}) => ({...state, value})),
);

export function showAddItemComponentReducer(state: AddItemComponentState = initialState, action: Action){
  return reducer(state, action);
}

export const selectShowAddItemComponentFeature = createFeatureSelector<AddItemComponentState>('showAddItemComponent');

export const selectShowAddItemComponentValue = createSelector(
  selectShowAddItemComponentFeature,
  state => state.value,
);
