import { Component, } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { CoursesViewModel } from '../../../app-core/model/viewmodel/courses';
import { CoursesService } from '../../../app-core/service/courses-service';
import { Utility } from '../../../app-core/utility';

@Component({
  selector: 'app-coursescreate',
  templateUrl: './create-courses.component.html',
  styleUrls: ['./create-courses.component.css']
})
export class CreateCoursesComponent {
  sub: Subscription;
  param = "";
  
  constructor(private route: ActivatedRoute, private router: Router, private service: CoursesService) {
    this.param = route.snapshot.queryParams["param"];
  }

  onSubmit(form: NgForm) {
    if (form.invalid === true) {
      return;
    }
    let vm = new CoursesViewModel();
    vm.name = Utility.toString(form.value.name);
    vm.kind = Utility.toInteger(form.value.kind);
    vm.day = Utility.toInteger(form.value.day);
    vm.dateCreated = Utility.toDateString(form.value.dateCreated);
    vm.price = Utility.toDecimal(form.value.price);
    //
    // business-logic
    //
    this.sub = this.service.insertCourse(vm).subscribe(response => {
      let q = { queryParams: { param: this.param } };
      this.router.navigate(["courses", "list"], q);
      console.log(JSON.stringify(vm));
    }); 
  }

  onCancel() {
    let q = { queryParams: { param: this.param } };
    this.router.navigate(["courses", "list"], q);
  }  

  ngOnDestroy() {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
}
