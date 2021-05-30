import { Component, Inject, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Observable, Subscription, of } from 'rxjs';

import { WorkshopStudentsViewModel } from '../../../app-core/model/viewmodel/workshopstudents';
import { WorkshopsService } from '../../../app-core/service/workshops-service';

@Component({
  selector: 'app-teacherscourses',
  templateUrl: './teachers-courses.component.html',
  styleUrls: ['./teachers-courses.component.css']
})
export class TeachersCoursesComponent {
  teachers: Observable<object>;

  constructor(private dialogRef: MatDialogRef<TeachersCoursesComponent>, @Inject(MAT_DIALOG_DATA) private data: any) {
    this.teachers = this.data;
  }

  onClose() {
    this.dialogRef.close("no");
  }

}
