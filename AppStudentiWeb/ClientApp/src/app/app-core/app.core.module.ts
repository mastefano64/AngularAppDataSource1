import { CommonModule } from '@angular/Common';
import { NgModule, ErrorHandler } from '@angular/core';

import { AuthGuard } from './authguard';
import { ErrorsHandler } from './error/errorshandler';
import { AuthService } from './service/auth-service';
import { TeachersBusiness } from '../feature-teachers/service/teachers-business';
import { StudentsBusiness } from '../feature-students/service/students-business';
import { TeachersService } from './service/teachers-service';
import { StudentsService } from './service/students-service';
import { CoursesService } from './service/courses-service';
import { WorkshopsService } from './service/workshops-service';
import { ProvincesService } from './service/province-service';

@NgModule({  
  imports: [
    CommonModule
  ],
  providers: [
    { provide: ErrorHandler, useClass: ErrorsHandler },
    AuthGuard,
    AuthService,
    TeachersBusiness,
    TeachersService,
    StudentsBusiness,
    StudentsService,
    CoursesService,
    WorkshopsService,
    ProvincesService
  ]
})
export class AppCoreModule { }
