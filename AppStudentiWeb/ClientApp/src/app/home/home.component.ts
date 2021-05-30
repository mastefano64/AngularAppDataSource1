import { Component } from '@angular/core';

import { AuthService } from '../app-core/service/auth-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(private auth: AuthService) { }

  onError1() {
    this.auth.error1().subscribe(response => {
      let ret = <any>response;     
    });
  }

  onError2() {
    this.auth.error2().subscribe(response => {
      let ret = <any>response;
    });
  }
}
