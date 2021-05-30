import { CommonModule } from '@angular/Common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { AppMaterialModule } from '../app.material.module';
import { AppSharedModule } from '../shared/app.shared.module';
import { AuthGuard } from '../app-core/authguard';
import { ListCoursesComponent } from './component/list-courses/list-courses.component';
import { SearchCoursesComponent } from './component/search-courses/search-courses.component';
import { WorkshopsCoursesComponent } from './component/workshops-courses/workshops-courses.component';
import { TeachersCoursesComponent } from './component/teachers-courses/teachers-courses.component';
import { CreateCoursesComponent } from './component/create-courses/create-courses.component';
import { EditCoursesComponent } from './component/edit-courses/edit-courses.component';

const routes: Routes = [
  { path: 'courses/list', component: ListCoursesComponent, canActivate: [AuthGuard] },
  { path: 'courses/create', component: CreateCoursesComponent, canActivate: [AuthGuard] },
  { path: 'courses/edit/:id', component: EditCoursesComponent, canActivate: [AuthGuard] },
];

@NgModule({
  declarations: [
    ListCoursesComponent,
    SearchCoursesComponent,
    WorkshopsCoursesComponent,
    TeachersCoursesComponent,
    CreateCoursesComponent,
    EditCoursesComponent
  ],
  entryComponents: [
    SearchCoursesComponent,
    WorkshopsCoursesComponent,
    TeachersCoursesComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    AppMaterialModule,
    AppSharedModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppCoursesModule { }
