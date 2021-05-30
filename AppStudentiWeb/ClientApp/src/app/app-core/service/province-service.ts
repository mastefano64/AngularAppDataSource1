import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";

import { ProvinceViewModel } from "../model/viewmodel/province";
import { Utility } from "../../app-core/utility";

@Injectable()
export class ProvincesService {
  constructor(private http: HttpClient) {}  

  getAllProvince(): Observable<object> {
    return this.http.get('/provinces/getallprovince');
  }
}
