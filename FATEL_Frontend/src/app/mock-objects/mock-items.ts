import {Item} from '../entities/item'
import {Unit} from "../entities/units";

export const ITEMS: Item[] = [
  {id: 1, name: 'Plank', width: 5, length: 4, unit: Unit.Meter, quantity: 2, note:'Old plank'},
  {id: 2, name: 'Doorbell', unit: Unit.Piece, quantity: 3, note:'Loud doorbells'},
  {id: 3, name: 'Window', width: 2, length: 2, unit: Unit.SquareMeter, quantity: 4, note:'2x2 window'},
  {id: 4, name: 'Hammer', unit: Unit.Piece, quantity: 2, note:'Just a hammer'},
]
