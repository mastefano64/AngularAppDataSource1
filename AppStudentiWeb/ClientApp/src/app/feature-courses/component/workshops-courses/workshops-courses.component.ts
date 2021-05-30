import { Component, Inject, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Observable, Subscription, of } from 'rxjs';

import { WorkshopStudentsViewModel } from '../../../app-core/model/viewmodel/workshopstudents';
import { WorkshopsService } from '../../../app-core/service/workshops-service';

@Component({
  selector: 'app-workshopscourses',
  templateUrl: './workshops-courses.component.html',
  styleUrls: ['./workshops-courses.component.css']
})
export class WorkshopsCoursesComponent {
  workshops: Observable<object>;

  constructor(private dialogRef: MatDialogRef<WorkshopsCoursesComponent>, @Inject(MAT_DIALOG_DATA) private data: any) {
    this.workshops = this.data;
  }

  onClose() {
    this.dialogRef.close("no");
  }

}
