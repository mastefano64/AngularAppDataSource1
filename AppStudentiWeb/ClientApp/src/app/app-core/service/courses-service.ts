import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";

import { CoursesSearch } from "../model/search/courses";
import { CoursesViewModel } from "../model/viewmodel/courses";
import { Utility } from "../../app-core/utility";

@Injectable()
export class CoursesService {
  constructor(private http: HttpClient) {}

  fetchData(search: CoursesSearch, page: number, pagesize: number, orderbycolumn: string, orderbydirection: string): Observable<object> {
    if (search) {
      const params = new HttpParams()
        .set('name', Utility.toString(search.name))
        .set('kind', Utility.toString(search.kind))
        .set('day', Utility.toString(search.day))
        .set('dateCreated', Utility.toString(search.dateCreated))
        .set('price', Utility.toString(search.price))
        .set('page', page.toString())
        .set('pagesize', pagesize.toString())
        .set('orderbycolumn', orderbycolumn)
        .set('orderbydirection', orderbydirection);

      return this.http.get('/courses/ajaxpaging', { params: params });
    }
    else {
      const params = new HttpParams()       
        .set('page', page.toString())
        .set('pagesize', pagesize.toString())
        .set('orderbycolumn', orderbycolumn)
        .set('orderbydirection', orderbydirection);

      return this.http.get('/courses/ajaxpaging', { params: params });
    }   
  }

  getAllCourse(): Observable<object> {
    return this.http.get('/courses/getallcourse');
  }

  getCourseById(id: number): Observable<object> {
    return this.http.get('/courses/getcoursebyid/' + id);
  }

  insertCourse(entity: CoursesViewModel): Observable<object> {
    return this.http.post('/courses/insertcourse/', entity);
  }

  updateCourse(entity: CoursesViewModel): Observable<object> {
    return this.http.post('/courses/updatecourse/', entity);
  }

  deleteCourse(id: number): Observable<object> {
    return this.http.get('/courses/deletecourse/' + id);
  }

  getTeachersByCourse(id: number): Observable<object> {
    return this.http.get('/courses/getteachersbycourse/' + id);
  }

  getWorkshopsByCourse(id: number): Observable<object> {
    return this.http.get('/courses/getworkshopsbycourse/' + id);
  }
}
