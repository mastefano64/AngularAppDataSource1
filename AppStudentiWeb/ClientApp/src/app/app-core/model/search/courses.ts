
export class CoursesSearch {
  name: string;
  kind: number;
  day: number
  dateCreated: string;
  price: number;

  constructor() {
    this.name = '';
    this.kind = 0;
    this.day = 0;
    this.dateCreated = '';
    this.price = 0;
  }

  setFastSearch(value: string) {
    this.clear();
    if (value === undefined) {
      value = "";
    }
    this.name = value.trim();
  }

  isEmpty(): boolean {
    if (this.name.trim() === "" && this.kind.toString() === "0" && this.day.toString() === "0" &&
                     this.dateCreated.trim() === "" && this.price.toString() === "0")
      return true;
    else return false;
  }

  trimAll() {
    this.name = this.name.trim();    
  }

  set(param: any) {
    this.name = param.name;
    this.kind = param.kind;
    this.day = param.day;
    this.dateCreated = param.dateCreated;
    this.price = param.price;
  }

  clear() {
    this.name = '';
    this.kind = 0;
    this.day = 0;
    this.dateCreated = '';
    this.price = 0;
  }
} 
