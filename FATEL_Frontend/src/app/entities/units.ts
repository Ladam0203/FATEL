export enum Unit {
  Meter,
  SquareMeter,
  Piece,
}

export namespace Unit {
  export function keys(): Array<string>{
    var keys = Object.keys(Unit);
    return keys.slice(keys.length / 2, keys.length-1);
  }
}
