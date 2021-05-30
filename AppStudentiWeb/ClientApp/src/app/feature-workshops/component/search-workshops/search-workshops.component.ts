import { Component, Inject } from '@angular/core';
import { NgForm, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

import { WorkshopsSearch } from '../../../app-core/model/search/workshops';
import { CoursesService } from '../../../app-core/service/courses-service';
import { TeachersService } from '../../../app-core/service/teachers-service';
import { Utility } from '../../../app-core/utility';

@Component({
  selector: 'app-dialogsearch',
  templateUrl: './search-workshops.component.html',
  styleUrls: ['./search-workshops.component.css']
})
export class SearchWorkshopsComponent {  
  form: WorkshopsSearch;
  courses;
  teachers;

  constructor(private dialogRef: MatDialogRef<SearchWorkshopsComponent>, @Inject(MAT_DIALOG_DATA) private data: any,
                          private service1: CoursesService, private service2: TeachersService) {}

  ngOnInit() {
    this.form = new WorkshopsSearch();
    this.form.set(this.data);
    this.form.dateIn = <string>Utility.createDate(this.data.dateIn);
    this.form.dateFi = <string>Utility.createDate(this.data.dateFi);
    this.courses = this.service1.getAllCourse();
    this.teachers = this.service2.getAllTeacher();
  }

  onSearch(form: NgForm) {
    if (form.invalid === true) {
      return;
    }
    this.data.name = Utility.toString(form.value.name);
    this.data.courseId = Utility.toInteger(form.value.courseId); 
    this.data.teacherId = Utility.toInteger(form.value.teacherId);
    this.data.dateIn = Utility.toDateString(form.value.dateIn);
    this.data.dateFi = Utility.toDateString(form.value.dateFi);
    this.dialogRef.close("ok");
  }  

  onClose() {
    this.dialogRef.close("no");
  }

  onClear() {
    this.form.clear();
  }
}
