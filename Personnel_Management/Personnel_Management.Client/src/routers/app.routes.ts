import { RouterModule, Routes } from '@angular/router';
import { ViewEmployeeListComponent } from '../views/view-employee-list/view-employee-list.component';
import { LoginSignupComponent } from '../views/login-signup/login-signup.component';
import { NgModule } from '@angular/core';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/viewEmployeeList',
    pathMatch: 'full'
  },
  {
    path: 'login',
    title: 'Login',
    component: LoginSignupComponent
  },
  {
    path: 'viewEmployeeList',
    title: 'Xem Danh Sach Nhan Vien',
    component: ViewEmployeeListComponent
  }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
