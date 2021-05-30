import { Component, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatPaginator, MatSort, MatDialog, MatDialogConfig } from '@angular/material';
import { tap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { Subscription, merge } from "rxjs";

import { SearchTeachersComponent } from '../search-teachers/search-teachers.component';
import { SearchTextBoxComponent } from '../../../shared/component/searchtextbox/searchtextbox.component';
import { TeachersSearch } from '../../../app-core/model/search/teachers';
import { TeachersViewModel } from '../../../app-core/model/viewmodel/teachers';
import { TeachersDataSource } from '../../datasource/teachers-datasource';
import { TeachersService } from '../../../app-core/service/teachers-service';
import { QueryBackup } from '../../../app-core/datasource/querybackup';
import { ICommandResult } from '../../../app-core/error/errorvalidate';
import { Errors } from '../../../app-core/error/errors';
import { Popup } from '../../../app-core/error/popup';

@Component({
  selector: 'app-teacherslist',
  templateUrl: './list-teachers.component.html',
  styleUrls: ['./list-teachers.component.css']
})
export class ListTeachersComponent {
  search: TeachersSearch;
  datasource: TeachersDataSource;
  displayedColumns = ["teacherid", "name", "surname", "address", "cap", "city", "province", "x"];
  @ViewChild(SearchTextBoxComponent) searchtextbox: SearchTextBoxComponent;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  sub: Subscription;
  searchempty = true;
  param = "";

  constructor(private dialog: MatDialog, private router: Router,private route: ActivatedRoute, private service: TeachersService) {
    this.search = new TeachersSearch();
    this.datasource = new TeachersDataSource(this.service, this.search);
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
      this.loadTeachersPage();
      return;
    }
    this.datasource.loadPaggedData(0, 25, "teacherId");
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => {
      this.paginator.pageIndex = 0
    });
    merge(this.sort.sortChange, this.paginator.page).pipe(
      tap(() => {
        this.loadTeachersPage()
      })
    ).subscribe();
  }

  onTextboxKeyup(value) {
    this.paginator.pageIndex = 0;
    this.search.setFastSearch(value);
    this.loadTeachersPage();
  }

  onShowModalSearch() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '600px';
    dialogConfig.height = '550px';
    dialogConfig.data = this.search;
    this.searchtextbox.clearTextBox();
    let dialogRef = this.dialog.open(SearchTeachersComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(data => {
      if (data === "ok") {
        this.paginator.pageIndex = 0
        this.searchempty = this.search.isEmpty();
        this.loadTeachersPage();
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
    this.router.navigate(["teachers","create"], q2);
  }

  onEdit(item: TeachersViewModel) {
    let page = this.paginator.pageIndex;
    let pagesize = this.paginator.pageSize;
    let orderbycolumn = this.sort.active;
    let orderbydirection = this.sort.direction;
    let q1 = QueryBackup.backup(this.search, page, pagesize, orderbycolumn, orderbydirection);
    let q2 = { queryParams: { param: q1 } };
    this.router.navigate(["teachers", "edit", item.teacherId], q2);
  }

  onDelete(item: TeachersViewModel) {
    if (Popup.confirmDelete() === false) {
      return;
    }
    let id = item.teacherId;
    this.sub = this.service.deleteTeacher(id).subscribe(response => {
      Errors.showErrorIfNedded(<ICommandResult>response);
      //this.datasource.refresh(this.search);
      this.loadTeachersPage();
    });    
  }

  loadTeachersPage() {
    this.datasource.loadPaggedData(this.paginator.pageIndex, this.paginator.pageSize,
      this.sort.active, this.sort.direction);
  }

  ngOnDestroy() {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
}
