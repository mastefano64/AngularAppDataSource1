import { Component, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatPaginator, MatSort, MatDialog, MatDialogConfig } from '@angular/material';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Subscription, Observable, merge } from "rxjs";

import { SearchStudentsComponent } from '../search-students/search-students.component';
import { SearchTextBoxComponent } from '../../../shared/component/searchtextbox/searchtextbox.component';
import { StudentsSearch } from '../../../app-core/model/search/students';
import { StudentsViewModel } from '../../../app-core/model/viewmodel/students';
import { StudentsDataSource } from '../../datasource/students-datasource';
import { StudentsService } from '../../../app-core/service/students-service';
import { ProvincesService } from '../../../app-core/service/province-service';
import { QueryBackup } from '../../../app-core/datasource/querybackup';
import { ICommandResult } from '../../../app-core/error/errorvalidate';
import { Errors } from '../../../app-core/error/errors';
import { Popup } from '../../../app-core/error/popup';

@Component({
  selector: 'app-studentslist',
  templateUrl: './list-students.component.html',
  styleUrls: ['./list-students.component.css']
})
export class ListStudentsComponent {
  search: StudentsSearch;
  datasource: StudentsDataSource;
  displayedColumns = ["studentid", "name", "surname", "address", "cap", "city", "province", "x"];
  @ViewChild(SearchTextBoxComponent) searchtextbox: SearchTextBoxComponent;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  provinces: Observable<object>;
  sub: Subscription;
  searchempty = true;
  param = "";

  constructor(private dialog: MatDialog, private router: Router, private route: ActivatedRoute, private service1: StudentsService, private service2: ProvincesService) {
    this.search = new StudentsSearch();
    this.datasource = new StudentsDataSource(this.service1, this.search);
    this.param = route.snapshot.queryParams["param"];
  }

  ngOnInit() {
    if (this.param !== undefined) {
      let p = QueryBackup.restore(this.param);
      this.search.set(p.search);
      this.paginator.pageIndex = p.page;
      this.paginator.pageSize = p.pagesize;
      this.sort.active = p.orderbycolumn;
      this.sort.direction = p.orderbydirection;
      this.loadStudentsPage();
      return;
    }
    this.datasource.loadPaggedData(0, 25, "studentId");
    this.provinces = this.service2.getAllProvince();
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => {
      this.paginator.pageIndex = 0
    });  
    merge(this.sort.sortChange, this.paginator.page).pipe(
      tap(() => {
        this.loadStudentsPage()
      })
    ).subscribe();
  }

  onTextboxKeyup(value) {
    this.paginator.pageIndex = 0;
    this.search.setFastSearch(value);
    this.searchempty = this.search.isEmpty();
    this.loadStudentsPage();
  }

  onProvinceChange(event) {
    this.paginator.pageIndex = 0;
    this.search.province = event.value;
    this.searchempty = this.search.isEmpty();
    this.loadStudentsPage();
  }

  onShowModalSearch() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '600px';
    dialogConfig.height = '550px';
    dialogConfig.data = this.search;
    this.searchtextbox.clearTextBox();
    let dialogRef = this.dialog.open(SearchStudentsComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(data => {
      if (data === "ok") {
        this.paginator.pageIndex = 0
        this.searchempty = this.search.isEmpty();
        this.loadStudentsPage();
      }
      if (data === "no") {
        this.searchempty = this.search.isEmpty();
      }
    });
  }

  onCreate() {
    let page = this.paginator.pageIndex;
    let pagesize = this.paginator.pageSize;
    let orderbycolumn = this.sort.active;
    let orderbydirection = this.sort.direction;
    let q1 = QueryBackup.backup(this.search, page, pagesize, orderbycolumn, orderbydirection);
    let q2 = { queryParams: { param: q1 } };
    this.router.navigate(["students", "create"], q2);
  }

  onEdit(item: StudentsViewModel) {
    let page = this.paginator.pageIndex;
    let pagesize = this.paginator.pageSize;
    let orderbycolumn = this.sort.active;
    let orderbydirection = this.sort.direction;
    let q1 = QueryBackup.backup(this.search, page, pagesize, orderbycolumn, orderbydirection);
    let q2 = { queryParams: { param: q1 } };
    this.router.navigate(["students", "edit", item.studentId], q2);
  }

  onDelete(item: StudentsViewModel) {
    if (Popup.confirmDelete() === false) {
      return;
    }
    let id = item.studentId;
    this.sub = this.service1.deleteStudent(id).subscribe(response => {
      Errors.showErrorIfNedded(<ICommandResult>response);
      //this.datasource.refresh(this.search);
      this.loadStudentsPage();
    });
  }

  loadStudentsPage() {
    this.datasource.loadPaggedData(this.paginator.pageIndex, this.paginator.pageSize,
      this.sort.active, this.sort.direction);
  }

  ngOnDestroy() {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
}
