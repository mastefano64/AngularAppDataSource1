import { Component, Inject } from '@angular/core';
import { NgForm, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

import { StudentsSearch } from '../../../app-core/model/search/students';
import { ProvincesService } from '../../../app-core/service/province-service';
import { Utility } from '../../../app-core/utility';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-dialogsearch',
  templateUrl: './search-students.component.html',
  styleUrls: ['./search-students.component.css']
})
export class SearchStudentsComponent {  
  form: StudentsSearch;
  provinces: Observable<object>;

  constructor(private dialogRef: MatDialogRef<SearchStudentsComponent>, @Inject(MAT_DIALOG_DATA) private data: any, private service: ProvincesService) {}

  ngOnInit() {
    this.form = new StudentsSearch();
    this.form.set(this.data);
    this.provinces = this.service.getAllProvince();
  }

  onSearch(form: NgForm) {
    if (form.invalid === true) {
      return;
    }
    this.data.name = Utility.toString(form.value.name);
    this.data.surname = Utility.toString(form.value.surname);
    this.data.address = Utility.toString(form.value.address);
    this.data.cap = Utility.toString(form.value.cap);
    this.data.city = Utility.toString(form.value.city);
    this.data.province = Utility.toString(form.value.province);
    this.dialogRef.close("ok");
  }

  onClose() {
    this.dialogRef.close("no");
  }

  onClear() {
    this.form.clear();
  }
}
