import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { catchError, finalize } from "rxjs/operators";
import { BehaviorSubject, Observable, of } from "rxjs";

import { StudentsSearch } from "../../app-core/model/search/students";
import { StudentsViewModel } from "../../app-core/model/viewmodel/students";
import { BaseDataSource } from "../../app-core/datasource/basedatasource";
import { StudentsService } from "../../app-core/service/students-service";

export class StudentsDataSource extends BaseDataSource<StudentsSearch, StudentsViewModel, StudentsService> {
  constructor(service: StudentsService, search?: StudentsSearch) {
    super(service, search);
    this.result = { count: 0 };
  }
}

