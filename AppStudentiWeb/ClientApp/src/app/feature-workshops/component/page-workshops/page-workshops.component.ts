import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Subscription } from 'rxjs';

import { SearchWorkshopsComponent } from '../search-workshops/search-workshops.component';
import { StudentsWorkshopComponent } from '../students-workshops/students-workshops.component';
import { WorkshopsSearch } from '../../../app-core/model/search/workshops';
import { WorkshopsViewModel } from '../../../app-core/model/viewmodel/workshops';
import { WorkshopsDataSource } from '../../datasource/workshops-datasource';
import { WorkshopsService } from '../../../app-core/service/workshops-service';
import { PagingData } from '../../../app-core/datasource/pagingdata';
import { QueryBackup } from '../../../app-core/datasource/querybackup';
import { ICommandResult } from '../../../app-core/error/errorvalidate';
import { Errors } from '../../../app-core/error/errors';
import { Popup } from '../../../app-core/error/popup';

@Component({
  selector: 'app-workshopspage',
  templateUrl: './page-workshops.component.html',
  styleUrls: ['./page-workshops.component.css']
})
export class PageWorkshopsComponent {
  search: WorkshopsSearch;
  datasource: WorkshopsDataSource;
  pagingdata: PagingData;
  sub: Subscription;
  searchempty = true;
  param = "";
  workshops;

  constructor(private dialog: MatDialog, private router: Router, private route: ActivatedRoute, private service: WorkshopsService) {
    this.search = new WorkshopsSearch();
    this.datasource = new WorkshopsDataSource(this.service, this.search);
    this.pagingdata = new PagingData();
    this.param = route.snapshot.queryParams["param"];
  }

  ngOnInit() {
    this.workshops = this.datasource.connect(null);
    if (this.param !== undefined) {
      let p = QueryBackup.restore(this.param);
      this.search.set(p.search);
      this.datasource.loadPaggedData(p.page, p.pagesize, p.orderbycolumn, p.orderbydirection);
      return;
    }   
    this.datasource.loadPaggedData(0, 25, "workshopsId");
  }

  onShowModalSearch() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '600px';
    dialogConfig.height = '550px';
    dialogConfig.data = this.search;
    let dialogRef = this.dialog.open(SearchWorkshopsComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(data => {
      if (data === "ok") {
        this.searchempty = this.search.isEmpty();
        this.datasource.loadPaggedData(0, 25, "workshopsId");
      }
      if (data === "no") {
        this.searchempty = this.search.isEmpty();
      }
    });
  }

  onManageStudent(item) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '950px';
    dialogConfig.height = '550px';
    let students = this.service.getStudentsByWorkshop(item.workshopId);
    dialogConfig.data = { workshopId: item.workshopId, students: students };
    let dialogRef = this.dialog.open(StudentsWorkshopComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(data => {
      if (data === "ok") {       
      }
      if (data === "no") {       
      }
    });
  }

  onCreate() {
    let q = { queryParams: { param: this.datasource.queryBackup() } };
    this.router.navigate(["workshops", "create"], q);
  }

  onEdit(item: WorkshopsViewModel) {
    let q = { queryParams: { param: this.datasource.queryBackup() } };
    this.router.navigate(["workshops", "edit", item.workshopId], q);
  }

  onDelete(item: WorkshopsViewModel) {
    if (Popup.confirmDelete() === false) {
      return;
    }
    let id = item.workshopId;
    this.sub = this.service.deleteWorkshop(id).subscribe(response => {
      Errors.showErrorIfNedded(<ICommandResult>response);
      this.datasource.refresh();
    });
  }

  ngOnDestroy() {
    if (this.sub) this.sub.unsubscribe();
    this.workshops = this.datasource.disconnect(null);
  }
}
