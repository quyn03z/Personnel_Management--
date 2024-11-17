import { Routes } from '@angular/router';
import { ViewEmployeeListComponent } from '../views/manager/view-employee-list/view-employee-list.component';
import { LoginComponent } from '../views/login/login.component';
import { AdminComponent } from '../views/admin/admin.component';
import { ViewEmployeeDetailComponent } from '../views/view-employee-detail/view-employee-detail.component';
import { CreateThuongPhatComponent } from '../views/ThuongPhatService/create-thuong-phat/create-thuong-phat.component';
import { UpdateThuongPhatComponent } from '../views/ThuongPhatService/update-thuong-phat/update-thuong-phat.component';
import { EmployeesListComponent } from '../views/admin/EmployeesList/employeesList.component';
import { AddEmployeesComponent } from '../views/admin/EmployeesList/Add/addEmployee.component';
import { EditEmployeesComponent } from '../views/admin/EmployeesList/Edit/editEmployee.component';
import { DeparmentListComponent } from '../views/admin/DepartmentList/deparment-list.component';
import { AttendanceReportComponent } from '../views/manager/attendance-report/attendance-report.component';
import { ForgotPasswordComponent } from '../views/forgot-password/forgot-password.component';
import { ConfirmOtpComponent } from '../views/confirm-otp/confirm-otp.component';

import { ChangePasswordComponent } from '../views/change-password/change-password.component';
import { ProfileComponent } from '../views/profile/profile.component';
import { AddDepartmentComponent } from '../views/admin/DepartmentList/add-department/add-department.component';
import { EditDepartmentComponent } from '../views/admin/DepartmentList/edit-department/edit-department.component';
import { ViewDepartmentEmployeeComponent } from '../views/admin/DepartmentList/view-department-employee/view-department-employee.component';
import { DiemdanhComponent } from '../views/diemdanh/diemdanh.component';
import { LichnghiComponent } from '../views/admin/lichnghi/lichnghi.component';
import { AuthGuard } from '../services/auth.guard';
import { RoleGuard } from '../services/role.guard';

export const routes: Routes = [
    { path: '',redirectTo: '/diemdanh',pathMatch: 'full', },
    { path: 'login',title: 'Login',component: LoginComponent},
    { path: 'viewEmployeeDetail/:nhanVienId',title: 'Xem thông tin nhân viên',component: ViewEmployeeDetailComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin','Manager']}},
    { path: 'viewEmployeeList',title: 'Xem Danh Sach Nhan Vien',component: ViewEmployeeListComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin','Manager']}},
    { path: 'createThuongPhat/:nhanVienId',title: 'Tao thưởng phạt cho nhân viên',component: CreateThuongPhatComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin']} },
    { path: 'updateThuongPhat/:nhanVienId/:thuongPhatId',title: 'Cập nhật thưởng phạt',component: UpdateThuongPhatComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin']}},
    { path: 'attendanceReport/:nhanVienId',title: 'Xem điểm danh nhân viên',component: AttendanceReportComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin']}},

    { path: 'admin',title: 'admin',component: AdminComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin','Manager']},
        children : [
            { path: 'employeesList',title: 'employeesList',component: EmployeesListComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin']},
                children : [{ path: 'add',title: 'add', component: AddEmployeesComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin']}},] 
            },
            { path: 'departmentList',title: 'departmentList',component: DeparmentListComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin']},
                children : [
                    { path: 'add-department',title: 'AddDepartment',component: AddDepartmentComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin']}},
                    { path: 'edit-department/:id',title: 'Chỉnh sửa phòng ban',component: EditDepartmentComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin']}},
                    { path: 'view-department-employee/:id',title:'nhan vien trong phong ban',component:ViewDepartmentEmployeeComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin']}}
                ]
            }
        ] 
    },
    { path: 'forgot-password',title: 'Forgot-Password',component: ForgotPasswordComponent}, 
    { path: 'confirm-otp', title: 'Confirm-OTP', component: ConfirmOtpComponent },
    { path: 'change-password', title: 'Change-Paassword', component: ChangePasswordComponent },
    { path: 'profile', title: 'Profile', component: ProfileComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin', 'Employee','Manager']} },
    { path: 'editEmployee/:id',title: 'Edit', component: EditEmployeesComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin']}},
    { path: 'diemdanh', title: 'DiemDanh', component: DiemdanhComponent,},
    { path: 'lichnghi', title:'lich  nghi', component: LichnghiComponent,canActivate: [AuthGuard, RoleGuard],data:{roles: ['Admin']}},

];
