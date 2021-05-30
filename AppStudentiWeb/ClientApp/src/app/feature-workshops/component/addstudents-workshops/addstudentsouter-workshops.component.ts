import { Component, Input, Output, EventEmitter } from '@angular/core';

import { StudentsViewModel } from '../../../app-core/model/viewmodel/students';
import { StudentsService } from '../../../app-core/service/students-service';

@Component({
  selector: 'app-addstudentsbutton',
  templateUrl: './addstudentsouter-workshops.component.html',
  styleUrls: ['./addstudentsouter-workshops.component.css']
})
export class AddStudentsOuterComponent {
  @Output() addedstudent = new EventEmitter<any>();
  students: Array<StudentsViewModel> = [];
  isopen = false;
  search = "";
  selected;

  constructor(private service: StudentsService) {}

  onOpenClose() {
    this.isopen = !this.isopen;
  }

  onAddedStudent() {
    this.addedstudent.emit(this.selected);
    this.selected = undefined;
  }

  onTextboxKeyup(search) {
    this.search = search;
    this.service.getStudentByName(search).subscribe(response => {
      this.students = <any>response;
    });
  }

  onSelectItem(item) {
    this.selected = item;
    this.isopen = false;
  }

}
