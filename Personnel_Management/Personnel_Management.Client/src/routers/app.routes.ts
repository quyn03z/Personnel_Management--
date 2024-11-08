import { Routes } from '@angular/router';
import { ViewEmployeeListComponent } from '../views/view-employee-list/view-employee-list.component';
import { LoginComponent } from '../views/login/login.component';
import { AdminComponent } from '../views/admin/admin.component';
import { ViewEmployeeDetailComponent } from '../views/view-employee-detail/view-employee-detail.component';

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
        component: ViewEmployeeListComponent,
        children: [
            
        ]
    },
    {
        path: 'admin',
        title: 'admin',
        component: AdminComponent
    }

];
