import { Exception } from "./error/exception";
import { isMoment } from "moment";

export class Utility {
  public static isDate(value1: string): boolean {
    let valret = true;
    const regexp = '(0?[1-9]|[12][0-9]|3[01])/(0?[1-9]|1[012])/((19|20)\\d\\d)';
    try {
      if (!value1 || !value1.match(regexp)) {
        throw new Exception();
      }
      const numbers = value1.split('/');
      const value2 = numbers[2] + "/" + numbers[1] + "/" + numbers[0];
      if (isNaN(Date.parse(value2))) {
        throw new Exception();
      }
    }
    catch (e) {
      valret = false;
    }
    return valret;
  }

  public static createDate(amg: string, sep: string = '/'): Date | string {
    if (!amg)
      return '';
    const array = amg.split(sep);
    const y = +array[0];
    const m = +array[1];
    const d = +array[2];
    const date = new Date(y, m - 1, d);
    return date;
  }

  public static toString(value: any): string {
    let valret = '';
    if (value === '' || value === null || value === undefined) {
      return valret;
    }
    valret = value.toString().trim();
    return valret;
  }

  public static toInteger(value: any): number {
    let valret = 0;
    if (!value) {
      return valret;
    }
    const str = value.toString().trim();
    valret = parseInt(str, 10);
    return valret;
  }

  public static toDecimal(value: any, removecomma = true): number {
    let valret = 0;
    if (!value) {
      return valret;
    }
    const str = value.toString().trim();
    if (removecomma === true) {
      valret = parseFloat(str.replace(',', '.'));
    } else {
      valret = parseFloat(str);
    }
    return valret;
  }

  public static toDateString(value: any, sep: string = '/'): string {
    if (!value)
      return '';
    let amg = value;
    if (isMoment(value)) {
      const d = value.date();
      const m = value.months() + 1;
      const y = value.year();
      amg = y + sep + m + sep + d;
    }
    else if (value instanceof Date) {
      const d = value.getDate();
      const m = value.getMonth() + 1;
      const y = value.getFullYear();
      amg = y + sep + m + sep + d;
    }
    else {
      const index = amg.indexOf('T');
      if (index !== -1)
        amg = amg.substring(0, index);
    }
    return amg;
  }
}
