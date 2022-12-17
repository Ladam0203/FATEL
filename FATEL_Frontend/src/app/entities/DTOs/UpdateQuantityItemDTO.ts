import {Unit} from "../units";

export interface UpdateQuantityItemDTO {
  item: {
    id: number,
    name: string,
    length?: number | null;
    width?: number | null;
    unit: Unit;
    note?: string | null;
  },
  change: number;
}
