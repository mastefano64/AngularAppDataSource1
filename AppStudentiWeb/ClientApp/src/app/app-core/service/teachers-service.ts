import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";

import { TeachersSearch } from "../model/search/teachers";
import { TeachersViewModel } from "../model/viewmodel/teachers";
import { Utility } from "../../app-core/utility";

@Injectable()
export class TeachersService {
  constructor(private http: HttpClient) {}

  fetchData(search: TeachersSearch, page: number, pagesize: number, orderbycolumn: string, orderbydirection: string): Observable<object> {
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

      return this.http.get('/teachers/ajaxpaging', { params: params });
    }
    else {
      const params = new HttpParams()       
        .set('page', page.toString())
        .set('pagesize', pagesize.toString())
        .set('orderbycolumn', orderbycolumn)
        .set('orderbydirection', orderbydirection);

      return this.http.get('/teachers/ajaxpaging', { params: params });
    }    
  }

  getAllTeacher(): Observable<object> {
    return this.http.get('/teachers/getallteacher');
  }

  getTeacherByName(name: string): Observable<object> {
    return this.http.get('/teachers/getteacherbyname?name=' + name);
  }

  getTeacherById(id: number): Observable<object> {
    return this.http.get('/teachers/getteacherbyid/'+ id);
  }

  insertTeacher(entity: TeachersViewModel): Observable<object> {
    return this.http.post('/teachers/insertteacher/', entity);
  }

  updateTeacher(entity: TeachersViewModel): Observable<object> {
    return this.http.post('/teachers/updateteacher/', entity);
  }

  deleteTeacher(id: number): Observable<object> {
    return this.http.get('/teachers/deleteteacher/' + id);
  }
}
