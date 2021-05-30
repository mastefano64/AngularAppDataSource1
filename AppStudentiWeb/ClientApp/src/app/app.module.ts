import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from "@angular/common/http";

import { AppMaterialModule } from './app.material.module';
import { AppCoreModule } from './app-core/app.core.module';
import { AppRoutingModule } from './app.routing.module';

import { AppTeachersModule } from './feature-teachers/app.teachers.module';
import { AppStudentsModule } from './feature-students/app.students.module';
import { AppCoursesModule } from './feature-courses/app.courses.module';
import { AppWorkshopsModule } from './feature-workshops/app.workshops.module';
import { AppSharedModule } from './shared/app.shared.module';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    AppMaterialModule,
    AppCoreModule,
    AppTeachersModule,
    AppStudentsModule,
    AppCoursesModule,
    AppWorkshopsModule,
    AppRoutingModule,
    AppSharedModule,
  ],
  providers: [
  ],
  bootstrap: [AppComponent]  
})
export class AppModule { }
