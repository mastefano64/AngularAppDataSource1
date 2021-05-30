import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";

import { StudentsSearch } from "../model/search/students";
import { StudentsViewModel } from "../model/viewmodel/students";
import { Utility } from "../../app-core/utility";

@Injectable()
export class StudentsService {
  constructor(private http: HttpClient) {}

  fetchData(search: StudentsSearch, page: number, pagesize: number, orderbycolumn: string, orderbydirection: string): Observable<object> {
    if (search) {
      const params = new HttpParams()
        .set('name', Utility.toString(search.name))
        .set('surname', Utility.toString(search.surname))
        .set('address', Utility.toString(search.address))
        .set('cap', Utility.toString(search.cap))
        .set('city', Utility.toString(search.city))
        .set('province', Utility.toString(search.province))
        .set('page', page.toString())
        .set('pagesize', pagesize.toString())
        .set('orderbycolumn', orderbycolumn)
        .set('orderbydirection', orderbydirection);

      return this.http.get('/students/ajaxpaging', { params: params });
    }
    else {
      const params = new HttpParams()
        .set('page', page.toString())
        .set('pagesize', pagesize.toString())
        .set('orderbycolumn', orderbycolumn)
        .set('orderbydirection', orderbydirection);

      return this.http.get('/students/ajaxpaging', { params: params });
    }    
  }

  getAllStudent(): Observable<object> {
    return this.http.get('/students/getallstudent');
  }

  getStudentByName(name: string): Observable<object> {
    return this.http.get('/students/getstudentbyname?name=' + name);
  }

  getStudentById(id: number): Observable<object> {
    return this.http.get('/students/getstudentbyid/' + id);
  }

  insertStudent(entity: StudentsViewModel): Observable<object> {
    return this.http.post('/students/insertstudent/', entity);
  }

  updateStudent(entity: StudentsViewModel): Observable<object> {
    return this.http.post('/students/updatestudent/', entity);
  }

  deleteStudent(id: number): Observable<object> {
    return this.http.get('/students/deletestudent/' + id);
  }
}
