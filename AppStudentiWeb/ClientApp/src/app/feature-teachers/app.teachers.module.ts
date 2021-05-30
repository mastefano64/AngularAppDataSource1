import { CommonModule } from '@angular/Common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { AppMaterialModule } from '../app.material.module';
import { AppSharedModule } from '../shared/app.shared.module';
import { AuthGuard } from '../app-core/authguard';
import { ListTeachersComponent } from './component/list-teachers/list-teachers.component';
import { SearchTeachersComponent } from './component/search-teachers/search-teachers.component';
import { CreateTeachersComponent } from './component/create-teachers/create-teachers.component';
import { EditTeachersComponent } from './component/edit-teachers/edit-teachers.component';

const routes: Routes = [
  { path: 'teachers/list', component: ListTeachersComponent, canActivate: [AuthGuard] },
  { path: 'teachers/create', component: CreateTeachersComponent, canActivate: [AuthGuard] },
  { path: 'teachers/edit/:id', component: EditTeachersComponent, canActivate: [AuthGuard] },
];

@NgModule({
  declarations: [
    ListTeachersComponent,
    SearchTeachersComponent,
    CreateTeachersComponent,
    EditTeachersComponent
  ],
  entryComponents: [
    SearchTeachersComponent
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
export class AppTeachersModule { }
