import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { catchError, finalize } from "rxjs/operators";
import { BehaviorSubject, Observable, of } from "rxjs";

import { TeachersSearch } from "../../app-core/model/search/teachers";
import { TeachersViewModel } from "../../app-core/model/viewmodel/teachers";
import { BaseDataSource } from "../../app-core/datasource/basedatasource";
import { TeachersService } from "../../app-core/service/teachers-service";

export class TeachersDataSource extends BaseDataSource<TeachersSearch, TeachersViewModel, TeachersService> {
  constructor(service: TeachersService, search?: TeachersSearch) {
    super(service, search);
    this.result = { count: 0 };
  }
}

