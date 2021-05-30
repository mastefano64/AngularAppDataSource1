import { Component, Inject } from '@angular/core';
import { NgForm, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

import { CoursesSearch } from '../../../app-core/model/search/courses';
import { Utility } from '../../../app-core/utility';

@Component({
  selector: 'app-dialogsearch',
  templateUrl: './search-courses.component.html',
  styleUrls: ['./search-courses.component.css']
})
export class SearchCoursesComponent {  
  form: CoursesSearch;
  dateCreated: any;

  constructor(private dialogRef: MatDialogRef<SearchCoursesComponent>, @Inject(MAT_DIALOG_DATA) private data: any) {}

  ngOnInit() {
    this.form = new CoursesSearch();
    this.form.set(this.data);
    this.form.dateCreated = <string>Utility.createDate(this.data.dateCreated);
  }

  onSearch(form: NgForm) {
    if (form.invalid === true) {
      return;
    }
    this.data.name = Utility.toString(form.value.name);
    this.data.kind = Utility.toInteger(form.value.kind);
    this.data.day = Utility.toInteger(form.value.day);
    this.data.dateCreated = Utility.toDateString(form.value.dateCreated);
    this.data.price = Utility.toDecimal(form.value.price); 
    this.dialogRef.close("ok");
  }  

  onClose() {
    this.dialogRef.close("no");
  }

  onClear() {
    this.form.clear();
  }
}
