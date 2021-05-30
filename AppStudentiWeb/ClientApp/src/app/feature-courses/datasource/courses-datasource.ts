import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { catchError, finalize } from "rxjs/operators";
import { BehaviorSubject, Observable, of } from "rxjs";

import { CoursesSearch } from "../../app-core/model/search/courses";
import { CoursesViewModel } from "../../app-core/model/viewmodel/courses";
import { BaseDataSource } from "../../app-core/datasource/basedatasource";
import { CoursesService } from "../../app-core/service/courses-service";

export class CoursesDataSource extends BaseDataSource<CoursesSearch, CoursesViewModel, CoursesService> {
  constructor(service: CoursesService, search?: CoursesSearch) {
    super(service, search);
    this.result = { count: 0 };
  }
}

