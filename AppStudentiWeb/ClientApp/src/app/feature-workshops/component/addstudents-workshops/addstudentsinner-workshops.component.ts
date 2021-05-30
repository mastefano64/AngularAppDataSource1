import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';

import { StudentsViewModel } from '../../../app-core/model/viewmodel/students';
import { StudentsService } from '../../../app-core/service/students-service';

@Component({
  selector: 'app-addstudentsoverlay',
  templateUrl: './addstudentsinner-workshops.component.html',
  styleUrls: ['./addstudentsinner-workshops.component.css']
})
export class AddStudentsInnerComponent {
  @Input() search: string;
  @Input() students: Array<StudentsViewModel>;
  @Output() textboxkeyup = new EventEmitter<string>();
  @Output() selectitem = new EventEmitter<any>();

  constructor() {}

  onTextboxKeyup(search) {
    this.textboxkeyup.emit(search);
  }

  onSelectItem(item) {
    this.selectitem.emit(item);
  }

}
