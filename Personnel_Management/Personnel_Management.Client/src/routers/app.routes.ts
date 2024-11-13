import { Routes } from '@angular/router';
import { ViewEmployeeListComponent } from '../views/view-employee-list/view-employee-list.component';
import { LoginComponent } from '../views/login/login.component';
import { AdminComponent } from '../views/admin/admin.component';
import { ViewEmployeeDetailComponent } from '../views/view-employee-detail/view-employee-detail.component';
import { CreateThuongPhatComponent } from '../views/ThuongPhatService/create-thuong-phat/create-thuong-phat.component';
import { UpdateThuongPhatComponent } from '../views/ThuongPhatService/update-thuong-phat/update-thuong-phat.component';
import { EmployeesListComponent } from '../views/admin/EmployeesList/employeesList.component';
import { AddEmployeesComponent } from '../views/admin/EmployeesList/Add/addEmployee.component';
import { EditEmployeesComponent } from '../views/admin/EmployeesList/Edit/editEmployee.component';
import { DeparmentListComponent } from '../views/admin/DepartmentList/deparment-list.component';
import { ForgotPasswordComponent } from '../views/forgot-password/forgot-password.component';
import { ConfirmOtpComponent } from '../views/confirm-otp/confirm-otp.component';

import { ChangePasswordComponent } from '../views/change-password/change-password.component';
import { ProfileComponent } from '../views/profile/profile.component';
import { AddDepartmentComponent } from '../views/admin/DepartmentList/add-department/add-department.component';
import { EditDepartmentComponent } from '../views/admin/DepartmentList/edit-department/edit-department.component';
import { ViewDepartmentEmployeeComponent } from '../views/admin/DepartmentList/view-department-employee/view-department-employee.component';
export const routes: Routes = [
    {
        path: '',
        redirectTo: '/viewEmployeeList',
        pathMatch: 'full'
    },
    {
        path: 'login',
        title: 'Login',
        component: LoginComponent
    },
    {
        path: 'viewEmployeeDetail/:nhanVienId',
        title: 'Xem thông tin nhân viên',
        component: ViewEmployeeDetailComponent
    },
    {
        path: 'viewEmployeeList',
        title: 'Xem Danh Sach Nhan Vien',
        component: ViewEmployeeListComponent
    },
    {
        path: 'createThuongPhat/:nhanVienId',
        title: 'Tao thưởng phạt cho nhân viên',
        component: CreateThuongPhatComponent 
    },
    {
        path: 'updateThuongPhat/:nhanVienId/:thuongPhatId',
        title: 'Cập nhật thưởng phạt',
        component: UpdateThuongPhatComponent
    },
    {
        path: 'admin',
        title: 'admin',
        component: AdminComponent,
        children : [
            {
                path: 'employeesList',
                title: 'employeesList',
                component: EmployeesListComponent,
                children : [
                    {
                        path: 'add',
                        title: 'add',
                        component: AddEmployeesComponent
                    },
                    {
                        path: 'edit',
                        title: 'edit',
                        component: EditEmployeesComponent
                    }
                ] 
            },
            {
                path: 'departmentList',
                title: 'departmentList',
                component: DeparmentListComponent,
                children : [
                    {
                        path: 'add-department',
                        title: 'AddDepartment',
                        component: AddDepartmentComponent,
                    },
                    {
                        path: 'edit-department/:id',
                        title: 'Chỉnh sửa phòng ban',
                        component: EditDepartmentComponent
                    },
                    {
                        path: 'view-department-employee/:id',
                        title:'nhan vien trong phong ban',
                        component:ViewDepartmentEmployeeComponent
                    }
                ]
            }
        ] 
    },
    {
        path: 'forgot-password',
        title: 'Forgot-Password',
        component: ForgotPasswordComponent
    }, 
    { path: 'confirm-otp', title: 'Confirm-OTP', component: ConfirmOtpComponent },
    { path: 'change-password', title: 'Change-Paassword', component: ChangePasswordComponent },
    { path: 'profile', title: 'Profile', component: ProfileComponent },

];
