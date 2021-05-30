import { Component, } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, Observable } from 'rxjs';

import { StudentsViewModel } from '../../../app-core/model/viewmodel/students';
import { StudentsService } from '../../../app-core/service/students-service';
import { ProvincesService } from '../../../app-core/service/province-service';
import { Utility } from '../../../app-core/utility';

@Component({
  selector: 'app-studentscreate',
  templateUrl: './create-students.component.html',
  styleUrls: ['./create-students.component.css']
})
export class CreateStudentsComponent {
  provinces: Observable<object>;
  sub: Subscription;
  param = "";
  
  constructor(private route: ActivatedRoute, private router: Router, private service1: StudentsService, private service2: ProvincesService) {
    this.param = route.snapshot.queryParams["param"];
  }

  ngOnInit() {
    this.provinces = this.service2.getAllProvince();
  }

  onSubmit(form: NgForm) {
    if (form.invalid === true) {
      return;
    }
    let vm = new StudentsViewModel();
    vm.name = Utility.toString(form.value.name);
    vm.surname = Utility.toString(form.value.surname);
    vm.address = Utility.toString(form.value.address);
    vm.cap = Utility.toString(form.value.cap);
    vm.city = Utility.toString(form.value.city);
    vm.province = Utility.toString(form.value.province);
    //
    // business-logic
    //
    this.sub = this.service1.insertStudent(vm).subscribe(response => {
      let q = { queryParams: { param: this.param } };
      this.router.navigate(["students", "list"], q);
      console.log(JSON.stringify(vm));
    });   
  }

  onCancel() {
    let q = { queryParams: { param: this.param } };
    this.router.navigate(["students", "list"], q);
  }  

  ngOnDestroy() {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
}
