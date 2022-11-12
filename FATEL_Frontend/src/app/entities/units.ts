export enum Unit {
  Meter,
  SquareMeter,
  Piece,
}

export namespace UnitUtil {
  export function abbreviations(index: number): String {
    switch (index) {
      case 0:
        return "m";
      case 1:
        return "m2";
      case 2:
        return "x";
    }
    return "";
  }
}
