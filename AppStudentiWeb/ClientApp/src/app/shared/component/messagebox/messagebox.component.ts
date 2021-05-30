import { Component, Input, ChangeDetectionStrategy } from '@angular/core';

import { ValidateStatus } from '../../../app-core/error/errorvalidate';

@Component({
  selector: 'app-messagebox',
  templateUrl: './messagebox.component.html',
  styleUrls: ['./messagebox.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MessageBoxComponent {
  @Input() color = "red";
  @Input() status: ValidateStatus;

  constructor() { }

}
