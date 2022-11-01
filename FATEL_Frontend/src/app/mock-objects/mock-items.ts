import {Item} from '../entities/item'
import {Unit} from "../entities/units";

export const ITEMS: Item[] = [
  {Id: 1, Name: 'Plank', Width: 5, Length: 4, Unit: Unit.Meter, Quantity: 2, Note:'Old plank'},
  {Id: 2, Name: 'Doorbell', Unit: Unit.Piece, Quantity: 3, Note:'Loud doorbells'},
  {Id: 3, Name: 'Window', Width: 2, Length: 2, Unit: Unit.SquareMeter, Quantity: 4, Note:'2x2 window'},
  {Id: 4, Name: 'Hammer', Unit: Unit.Piece, Quantity: 2, Note:'Just a hammer'},
]
