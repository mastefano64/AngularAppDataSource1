
export class WorkshopsSearch {
  name: string;
  dateIn: string;
  dateFi: string;
  courseId: number;
  teacherId: number;

  constructor() {
    this.name = '';
    this.dateIn = '';
    this.dateFi = '';
    this.courseId = 0;
    this.teacherId = 0;
  }

  setFastSearch(value: string) {
    this.clear();
    if (value === undefined) {
      value = "";
    }
    this.name = value.trim();
  }

  isEmpty(): boolean {
    if (this.name.trim() === "" && this.dateIn.trim() === "" && this.dateFi.trim() === "" &&
              this.courseId.toString() === "0" && this.teacherId.toString() === "0")
      return true;
    else return false;
  }

  trimAll() {
    this.name = this.name.trim();    
  }

  set(param: any) {
    this.name = param.name;
    this.dateIn = param.dateIn;
    this.dateFi = param.dateFi;
    this.courseId = param.courseId;
    this.teacherId = param.teacherId;
  }

  clear() {
    this.name = '';
    this.dateIn = '';
    this.dateFi = '';
    this.courseId = 0;
    this.teacherId = 0;
  }
} 
