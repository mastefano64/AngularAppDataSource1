import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { catchError, finalize } from "rxjs/operators";
import { BehaviorSubject, Observable, of } from "rxjs";

import { WorkshopsSearch } from "../../app-core/model/search/workshops";
import { WorkshopsViewModel } from "../../app-core/model/viewmodel/workshops";
import { BaseDataSource } from "../../app-core/datasource/basedatasource";
import { WorkshopsService } from "../../app-core/service/workshops-service";

export class WorkshopsDataSource extends BaseDataSource<WorkshopsSearch, WorkshopsViewModel, WorkshopsService> {
  constructor(service: WorkshopsService, search?: WorkshopsSearch) {
    super(service, search);
    this.result = { count: 0 };
  }
}

