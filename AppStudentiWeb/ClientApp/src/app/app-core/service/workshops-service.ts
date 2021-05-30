import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";

import { WorkshopsSearch } from "../model/search/workshops";
import { WorkshopsViewModel } from "../model/viewmodel/workshops";
import { WorkshopStudentsViewModel } from "../model/viewmodel/workshopstudents";
import { Utility } from "../../app-core/utility";

@Injectable()
export class WorkshopsService {
  constructor(private http: HttpClient) {}

  fetchData(search: WorkshopsSearch, page: number, pagesize: number, orderbycolumn: string, orderbydirection: string): Observable<object> {
    if (search) {
      const params = new HttpParams()
        .set('name', Utility.toString(search.name))
        .set('dateIn', Utility.toString(search.dateIn))
        .set('dateFi', Utility.toString(search.dateFi))
        .set('courseId', Utility.toString(search.courseId))
        .set('teacherId', Utility.toString(search.teacherId))
        .set('page', page.toString())
        .set('pagesize', pagesize.toString())
        .set('orderbycolumn', orderbycolumn)
        .set('orderbydirection', orderbydirection);

      return this.http.get('/workshops/ajaxpaging', { params: params });
    }
    else {
      const params = new HttpParams()      
        .set('page', page.toString())
        .set('pagesize', pagesize.toString())
        .set('orderbycolumn', orderbycolumn)
        .set('orderbydirection', orderbydirection);

      return this.http.get('/workshops/ajaxpaging', { params: params });
    }   
  }

  getWorkshopById(id: number): Observable<object> {
    return this.http.get('/workshops/getworkshopbyid/' + id);
  }

  insertWorkshop(entity: WorkshopsViewModel): Observable<object> {
    return this.http.post('/workshops/insertworkshop/', entity);
  }

  updateWorkshop(entity: WorkshopsViewModel): Observable<object> {
    return this.http.post('/workshops/updateworkshop/', entity);
  }

  deleteWorkshop(id: number): Observable<object> {
    return this.http.get('/workshops/deleteworkshop/' + id);
  }

  getStudentsByWorkshop(id: number): Observable<object> {
    return this.http.get('/workshops/getstudentsbyworkshop/' + id);
  }

  insertStudentsWorkshop(workshopId: number, studentId: number): Observable<object> {
    let params = "?workshopId=" + workshopId + "&studentId=" + studentId;
    return this.http.get('/workshops/insertstudentsworkshop' + params);
  }

  deleteStudentsWorkshop(workshopId: number, studentId: number): Observable<object> {
    let params = "?workshopId=" + workshopId + "&studentId=" + studentId;
    return this.http.get('/workshops/deletestudentsworkshop' + params);
  }
}
