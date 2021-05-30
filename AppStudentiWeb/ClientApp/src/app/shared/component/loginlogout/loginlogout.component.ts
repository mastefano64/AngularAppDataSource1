import { Component, Input, Output, EventEmitter, ChangeDetectionStrategy  } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../../../app-core/service/auth-service';

@Component({
  selector: 'app-loginlogout',
  templateUrl: './loginlogout.component.html',
  styleUrls: ['./loginlogout.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginLogoutComponent {
  @Input() authenticated = false;
  @Output() login = new EventEmitter();
  @Output() logout = new EventEmitter();

  constructor() { } 

  onLogin() {
    this.login.emit();
  }

  onLogout() {
    this.logout.emit();
  }
}
