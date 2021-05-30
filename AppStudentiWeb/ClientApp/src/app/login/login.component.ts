import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../app-core/service/auth-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username = "username1";
  password = "password1";
  invalid = false;

  constructor(private auth: AuthService, private router: Router) { }

  onLogin() {
    this.auth.login(this.username, this.password).subscribe(response => {
      let status = <any>response;
      if (status === false) {
        this.invalid = true;
        return;
      }
      this.router.navigate(['home']);
    });
  }
}
