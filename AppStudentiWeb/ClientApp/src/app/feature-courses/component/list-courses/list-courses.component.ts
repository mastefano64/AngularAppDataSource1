import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { Subscription } from 'rxjs';

import { SearchCoursesComponent } from '../search-courses/search-courses.component';
import { TeachersCoursesComponent } from '../teachers-courses/teachers-courses.component';
import { WorkshopsCoursesComponent } from '../workshops-courses/workshops-courses.component';
import { CoursesSearch } from '../../../app-core/model/search/courses';
import { CoursesViewModel } from '../../../app-core/model/viewmodel/courses';
import { CoursesDataSource } from '../../datasource/courses-datasource';
import { CoursesService } from '../../../app-core/service/courses-service';
import { PagingData } from '../../../app-core/datasource/pagingdata';
import { QueryBackup } from '../../../app-core/datasource/querybackup';
import { ICommandResult } from '../../../app-core/error/errorvalidate';
import { Errors } from '../../../app-core/error/errors';
import { Popup } from '../../../app-core/error/popup';

@Component({
  selector: 'app-courseslist',
  templateUrl: './list-courses.component.html',
  styleUrls: ['./list-courses.component.css']
})
export class ListCoursesComponent {
  search: CoursesSearch;
  datasource: CoursesDataSource;
  pagingdata: PagingData;
  sub: Subscription;
  searchempty = true;
  param = "";
  courses;

  constructor(private dialog: MatDialog, private router: Router, private route: ActivatedRoute, private service: CoursesService) {
    this.search = new CoursesSearch();
    this.datasource = new CoursesDataSource(this.service, this.search);
    this.pagingdata = new PagingData();
    this.param = route.snapshot.queryParams["param"];
  }

  ngOnInit() {
    this.courses = this.datasource.connect(null);
    if (this.param !== undefined) {
      let p = QueryBackup.restore(this.param);
      this.search.set(p.search);
      this.datasource.loadPaggedData(p.page, p.pagesize, p.orderbycolumn, p.orderbydirection);
      return;
    }   
    this.datasource.loadPaggedData(0, 25, "coursesId");
  }

  onShowModalSearch() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '600px';
    dialogConfig.height = '550px';
    dialogConfig.data = this.search;
    let dialogRef = this.dialog.open(SearchCoursesComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(data => {
      if (data === "ok") {
        this.searchempty = this.search.isEmpty();
        this.datasource.loadPaggedData(0, 25, "coursesId");
      }
      if (data === "no") {
        this.searchempty = this.search.isEmpty();
      }
    });
  }

  onTeachersDetail(item: CoursesViewModel) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '950px';
    dialogConfig.height = '550px';
    dialogConfig.data = this.service.getTeachersByCourse(item.courseId);
    let dialogRef = this.dialog.open(TeachersCoursesComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(data => {
      if (data === "ok") {
      }
      if (data === "no") {
      }
    });
  }

  onWorkshopsDetail(item: CoursesViewModel) {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '950px';
    dialogConfig.height = '550px';
    dialogConfig.data = this.service.getWorkshopsByCourse(item.courseId);
    let dialogRef = this.dialog.open(WorkshopsCoursesComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(data => {
      if (data === "ok") {
      }
      if (data === "no") {
      }
    });
  } 

  onCreate() {
    let q = { queryParams: { param: this.datasource.queryBackup() } };
    this.router.navigate(["courses", "create"], q);
  }

  onEdit(item: CoursesViewModel) {   
    let q = { queryParams: { param: this.datasource.queryBackup() } };
    this.router.navigate(["courses", "edit", item.courseId], q);
  }

  onDelete(item: CoursesViewModel) {
    if (Popup.confirmDelete() === false) {
      return;
    }
    let id = item.courseId;
    this.sub = this.service.deleteCourse(id).subscribe(response => {
      Errors.showErrorIfNedded(<ICommandResult>response);
      this.datasource.refresh();
    });
  }

  ngOnDestroy() {
    if (this.sub) this.sub.unsubscribe();
    this.courses = this.datasource.disconnect(null);
  }
}
