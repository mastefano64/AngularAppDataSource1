
export class TeachersSearch {
  name: string;
  surname: string
  address: string;
  cap: string;
  city: string;
  province: string;  

  constructor() {
    this.name = '';
    this.surname = '';
    this.address = '';
    this.cap = '';
    this.city = '';
    this.province = '';
  }

  setFastSearch(value: string) {
    this.clear();
    if (value === undefined) {
      value = "";
    }
    this.surname = value.trim();
  }

  isEmpty(): boolean {
    if (this.name.trim() === "" && this.surname.trim() === "" && this.address.trim() === "" &&
           this.cap.trim() === "" && this.city.trim() === "" && this.province.trim() === "")
      return true;
    else return false;
  }

  trimAll() {
    this.name = this.name.trim();
    this.surname = this.surname.trim();
    this.address = this.address.trim();
    this.cap = this.cap.trim();
    this.city = this.city.trim();
    this.province = this.province.trim();
  }

  set(param: any) {
    this.name = param.name;
    this.surname = param.surname;
    this.address = param.address;
    this.cap = param.cap.trim();
    this.city = param.city.trim();
    this.province = param.province;
  }

  clear() {
    this.name = '';
    this.surname = '';
    this.address = '';
    this.cap = '';
    this.city = '';
    this.province = '';
  }
} 
