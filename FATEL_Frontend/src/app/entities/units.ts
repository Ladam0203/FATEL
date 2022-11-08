export enum Unit {
  Meter,
  SquareMeter,
  Piece,
}

export namespace Unit {
  export function keys(): Array<string> {
    var keys = Object.keys(Unit);
    return keys.slice(keys.length / 2, keys.length - 1);
  }

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

  /*export function abbreviations(key: string): String {
    switch (key) {
      case "Meter":
        return "m";
      case "SquareMeter":
        return "m2";
      case "Piece":
        return "x";
    }
    return "";
  }*/
}
