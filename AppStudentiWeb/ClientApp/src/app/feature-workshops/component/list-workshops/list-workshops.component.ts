import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-workshopslist',
  templateUrl: './list-workshops.component.html',
  styleUrls: ['./list-workshops.component.css']
})
export class ListWorkshopsComponent {
  @Input() workshops;
  @Output() edit = new EventEmitter<any>();
  @Output() delete = new EventEmitter<any>();
  @Output() managestudent = new EventEmitter<any>();

  constructor() { }

  onManagestudentItem(item) {
    this.managestudent.emit(item);
  }

  onEditItem(item) {
    this.edit.emit(item);
  }

  onDeleteItem(item) {
    this.delete.emit(item);
  }
}
