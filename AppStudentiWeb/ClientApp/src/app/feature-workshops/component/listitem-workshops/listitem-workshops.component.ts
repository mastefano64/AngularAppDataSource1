import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-workshopslistitem',
  templateUrl: './listitem-workshops.component.html',
  styleUrls: ['./listitem-workshops.component.css']
})
export class ListitemWorkshopsComponent {
  @Input() workshop;
  @Output() edititem = new EventEmitter<any>();
  @Output() deleteitem = new EventEmitter<any>();
  @Output() managestudentitem = new EventEmitter<any>();

  constructor() { }

  ngOnInit() {
  }

  onEditItem(item) {
    this.edititem.emit(item);
  }

  onDeleteItem(item) {
    this.deleteitem.emit(item);
  }

  onManageStudentItem(item) {
    this.managestudentitem.emit(item);
  }

}
