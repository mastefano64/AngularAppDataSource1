import { CommonModule } from '@angular/Common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { AppMaterialModule } from '../app.material.module';
import { AppSharedModule } from '../shared/app.shared.module';
import { AuthGuard } from '../app-core/authguard';
import { PageWorkshopsComponent } from './component/page-workshops/page-workshops.component';
import { ListWorkshopsComponent } from './component/list-workshops/list-workshops.component';
import { ListitemWorkshopsComponent } from './component/listitem-workshops/listitem-workshops.component';
import { SearchWorkshopsComponent } from './component/search-workshops/search-workshops.component';
import { AddStudentsOuterComponent } from './component/addstudents-workshops/addstudentsouter-workshops.component';
import { AddStudentsInnerComponent } from './component/addstudents-workshops/addstudentsinner-workshops.component';
import { StudentsWorkshopComponent } from './component/students-workshops/students-workshops.component';
import { CreateWorkshopsComponent } from './component/create-workshops/create-workshops.component';
import { EditWorkshopsComponent } from './component/edit-workshops/edit-workshops.component';

const routes: Routes = [
  { path: 'workshops/list', component: PageWorkshopsComponent, canActivate: [AuthGuard] },
  { path: 'workshops/create', component: CreateWorkshopsComponent, canActivate: [AuthGuard] },
  { path: 'workshops/edit/:id', component: EditWorkshopsComponent, canActivate: [AuthGuard] },
];

@NgModule({
  declarations: [
    PageWorkshopsComponent,
    ListWorkshopsComponent,
    ListitemWorkshopsComponent,
    SearchWorkshopsComponent,
    AddStudentsOuterComponent,
    AddStudentsInnerComponent,
    StudentsWorkshopComponent,
    CreateWorkshopsComponent,
    EditWorkshopsComponent
  ],
  entryComponents: [
    SearchWorkshopsComponent,
    StudentsWorkshopComponent,
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
export class AppWorkshopsModule { }
