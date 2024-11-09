import { RouterModule, Routes } from '@angular/router';
import { ViewEmployeeListComponent } from '../views/view-employee-list/view-employee-list.component';
import { LoginSignupComponent } from '../views/login-signup/login-signup.component';
import { NgModule } from '@angular/core';
import { SignupComponent } from '../views/signup/signup.component';
import { ForgotPasswordComponent } from '../views/forgot-password/forgot-password.component';
import { ConfirmOtpComponent } from '../views/confirm-otp/confirm-otp.component';
import { ChangePasswordComponent } from '../views/change-password/change-password.component';
import { ProfileComponent } from '../views/profile/profile.component';

export const routes: Routes = [
  { path: '', redirectTo: '/viewEmployeeList', pathMatch: 'full' },
  { path: 'login', title: 'Login', component: LoginSignupComponent },
  { path: 'viewEmployeeList', title: 'Xem Danh Sach Nhan Vien', component: ViewEmployeeListComponent },
  { path: 'signup', title: 'Signup', component: SignupComponent },
  { path: 'forgot-password', title: 'Forgot-Password', component: ForgotPasswordComponent },
  { path: 'confirm-otp', title: 'Confirm-OTP', component: ConfirmOtpComponent },
  { path: 'change-password', title: 'Change-Password', component: ChangePasswordComponent },
  { path: 'profile', title: 'Profile', component: ProfileComponent }

  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
