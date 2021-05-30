import { Component, } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { WorkshopsViewModel } from '../../../app-core/model/viewmodel/workshops';
import { WorkshopsService } from '../../../app-core/service/workshops-service';
import { CoursesService } from '../../../app-core/service/courses-service';
import { TeachersService } from '../../../app-core/service/teachers-service';
import { Utility } from '../../../app-core/utility';

@Component({
  selector: 'app-workshopscreate',
  templateUrl: './create-workshops.component.html',
  styleUrls: ['./create-workshops.component.css']
})
export class CreateWorkshopsComponent {
  sub: Subscription;
  param = "";
  courses;
  teachers;
  
  constructor(private route: ActivatedRoute, private router: Router, private service1: WorkshopsService,
                 private service2: CoursesService, private service3: TeachersService) {
    this.param = route.snapshot.queryParams["param"];
  }

  ngOnInit() {
    this.courses = this.service2.getAllCourse();
    this.teachers = this.service3.getAllTeacher();
  }

  onSubmit(form: NgForm) {
    if (form.invalid === true) {
      return;
    }
    const vm = new WorkshopsViewModel();
    vm.name = Utility.toString(form.value.name);
    vm.courseId = Utility.toInteger(form.value.courseId);
    vm.teacherId = Utility.toInteger(form.value.teacherId);
    vm.dateIn = Utility.toDateString(form.value.dateIn);
    vm.dateFi = Utility.toDateString(form.value.dateFi);
    //
    // business-logic
    //
    this.sub = this.service1.insertWorkshop(vm).subscribe(response => {
      let q = { queryParams: { param: this.param } };
      this.router.navigate(["workshops", "list"], q);
      console.log(JSON.stringify(vm));
    });   
  }

  onCancel() {
    let q = { queryParams: { param: this.param } };
    this.router.navigate(["workshops", "list"], q);
  }  

  ngOnDestroy() {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
}
