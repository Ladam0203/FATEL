import {createAction, props, createReducer, on, Action, createFeatureSelector, createSelector} from "@ngrx/store";

export const setSearchbarQuery = createAction('[Filter-Bar Component] Set', props<{ value: string }>());

export interface SearchbarQueryState {
  value: string;
}

export const initialState: SearchbarQueryState = {
  value: '',
};

export const reducer = createReducer(
  initialState,
  on(setSearchbarQuery, (state, {value}) => ({...state, value})),
);

export function searchbarQueryReducer(state: SearchbarQueryState = initialState, action: Action) {
  return reducer(state, action);
}

export const selectSearchbarQueryFeature = createFeatureSelector<SearchbarQueryState>('searchbarQuery');

export const selectSearchbarQueryValue = createSelector(
  selectSearchbarQueryFeature,
  state => state.value,
);
