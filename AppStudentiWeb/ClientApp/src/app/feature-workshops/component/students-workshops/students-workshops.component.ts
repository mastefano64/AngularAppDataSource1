import { Component, Inject, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Observable, Subscription, of } from 'rxjs';

import { WorkshopStudentsViewModel } from '../../../app-core/model/viewmodel/workshopstudents';
import { StudentsViewModel } from '../../../app-core/model/viewmodel/students';
import { WorkshopsService } from '../../../app-core/service/workshops-service';
import { Popup } from '../../../app-core/error/popup';

@Component({
  selector: 'app-studentsworkshops',
  templateUrl: './students-workshops.component.html',
  styleUrls: ['./students-workshops.component.css']
})
export class StudentsWorkshopComponent {
  @Output() studentadded = new EventEmitter<any>();
  sub: Subscription;
  workshopId: number;
  students: Observable<object>;

  constructor(private dialogRef: MatDialogRef<StudentsWorkshopComponent>, @Inject(MAT_DIALOG_DATA) private data: any, private service: WorkshopsService) {
    this.workshopId = this.data.workshopId;
    this.students = this.data.students;
  }

  onAddedStudent(item: StudentsViewModel) {
    let id1 = this.workshopId;
    let id2 = item.studentId;
    this.service.insertStudentsWorkshop(id1, id2).subscribe(response => {
      this.students = of((<any>response).result);
    });
  }

  onDelete(item: WorkshopStudentsViewModel) {
    if (Popup.confirmDelete() === false) {
      return;
    }
    let id1 = item.workshopId;
    let id2 = item.studentId;
    this.sub = this.service.deleteStudentsWorkshop(id1, id2).subscribe(response => {
      this.students = this.service.getStudentsByWorkshop(item.workshopId);
    });
  }

  onClose() {
    this.dialogRef.close("no");
  }

  ngOnDestroy() {
    if (this.sub)
      this.sub.unsubscribe();
  }
}
