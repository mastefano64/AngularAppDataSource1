import { Component, } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription, Observable } from 'rxjs';

import { TeachersViewModel } from '../../../app-core/model/viewmodel/teachers';
import { TeachersService } from '../../../app-core/service/teachers-service';
import { ProvincesService } from '../../../app-core/service/province-service';
import { TeachersBusiness } from '../../service/teachers-business';
import { ValidateStatus, ValidateMode, ICommandResult } from '../../../app-core/error/errorvalidate';
import { Utility } from '../../../app-core/utility';

@Component({
  selector: 'app-teacherscreate',
  templateUrl: './create-teachers.component.html',
  styleUrls: ['./create-teachers.component.css']
})
export class CreateTeachersComponent {
  status = new ValidateStatus();
  provinces: Observable<object>;
  sub: Subscription;
  param = "";
  
  constructor(private route: ActivatedRoute, private router: Router, private logic: TeachersBusiness, private service1: TeachersService, private service2: ProvincesService) {
    this.param = route.snapshot.queryParams["param"];
  }

  ngOnInit() {
    this.provinces = this.service2.getAllProvince();
  }

  onSubmit(form: NgForm) {
    if (form.invalid === true) {
      return;
    }
    let vm = new TeachersViewModel();
    vm.name = Utility.toString(form.value.name);
    vm.surname = Utility.toString(form.value.surname);
    vm.address = Utility.toString(form.value.address);
    vm.cap = Utility.toString(form.value.cap);
    vm.city = Utility.toString(form.value.city);
    vm.province = Utility.toString(form.value.province);
    //
    this.status = this.logic.validate(ValidateMode.Create, vm);
    if (this.status.haserror === true) {
      return;
    }
    //
    this.sub = this.service1.insertTeacher(vm).subscribe(response => {
      let result = (<ICommandResult>response);
      if (result.hasError === true) {
        let status = new ValidateStatus();
        status.setErrorsFromResult(result);
        this.status = status;
        return;
      }
      let q = { queryParams: { param: this.param } };
      this.router.navigate(["teachers", "list"], q);
      console.log(JSON.stringify(vm));
    });   
  }

  onCancel() {
    let q = { queryParams: { param: this.param } };
    this.router.navigate(["teachers", "list"], q);
  }  

  ngOnDestroy() {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
}
