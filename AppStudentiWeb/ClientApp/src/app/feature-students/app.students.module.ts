import { CommonModule } from '@angular/Common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { AppMaterialModule } from '../app.material.module';
import { AppSharedModule } from '../shared/app.shared.module';
import { AuthGuard } from '../app-core/authguard';
import { ListStudentsComponent } from './component/list-students/list-students.component';
import { SearchStudentsComponent } from './component/search-students/search-students.component';
import { CreateStudentsComponent } from './component/create-students/create-students.component';
import { EditStudentsComponent } from './component/edit-students/edit-students.component';

const routes: Routes = [
  { path: 'students/list', component: ListStudentsComponent, canActivate: [AuthGuard] },
  { path: 'students/create', component: CreateStudentsComponent, canActivate: [AuthGuard] },
  { path: 'students/edit/:id', component: EditStudentsComponent, canActivate: [AuthGuard] },
];

@NgModule({
  declarations: [
    ListStudentsComponent,
    SearchStudentsComponent,
    CreateStudentsComponent,
    EditStudentsComponent
  ],
  entryComponents: [
    SearchStudentsComponent
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
export class AppStudentsModule { }
