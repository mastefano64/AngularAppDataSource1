import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../app-core/service/auth-service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  menutype: number = 1;
  authenticated = false;

  constructor(private auth: AuthService, private router: Router) { }

  ngOnInit() {
    this.auth.authenticatedAsync.subscribe(response => {
      this.authenticated = <any>response;
    });
  }

  onLogin(): void {
    this.router.navigate(['login']);
  }

  onLogout(): void {
    this.auth.logout().subscribe(response => {
      this.router.navigate(['home']);
    });
  }

  onPageChange(type): void {
    this.menutype = type;
  }
}
