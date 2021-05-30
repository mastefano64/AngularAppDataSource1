import { CommonModule } from '@angular/Common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { AppMaterialModule } from '../app.material.module';
import { ButtonPaginationComponent } from './component/buttonpagination/buttonpagination.component';
import { TableSortColComponent } from './component/tablesortcol/tablesortcol.component';
import { LoginLogoutComponent } from './component/loginlogout/loginlogout.component';
import { SearchTextBoxComponent } from './component/searchtextbox/searchtextbox.component';
import { MessageBoxComponent } from './component/messagebox/messagebox.component';

import { IntValidatorDirective } from './validators/int.validator';
import { DecimalValidatorDirective } from './validators/decimal.validator';
import { DateValidatorDirective } from './validators/date.validator';
import { SearchButtonDirective } from './directive/searchempty.directive';
import { DecNumberPipe } from './pipe/decnumber.pipe';

@NgModule({
  declarations: [
    ButtonPaginationComponent,
    TableSortColComponent,
    LoginLogoutComponent,
    SearchTextBoxComponent,
    MessageBoxComponent,
    IntValidatorDirective,
    DecimalValidatorDirective,
    DateValidatorDirective,
    SearchButtonDirective,
    DecNumberPipe
  ],
  imports: [
    CommonModule,
    FormsModule,
    AppMaterialModule
  ],
  exports: [
    ButtonPaginationComponent,
    TableSortColComponent,
    LoginLogoutComponent,
    SearchTextBoxComponent,
    MessageBoxComponent,
    IntValidatorDirective,
    DecimalValidatorDirective,
    DateValidatorDirective,
    SearchButtonDirective,
    DecNumberPipe
  ]
})
export class AppSharedModule { }
