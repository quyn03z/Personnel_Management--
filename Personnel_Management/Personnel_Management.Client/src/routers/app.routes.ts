import { Routes } from '@angular/router';
import { ViewEmployeeListComponent } from '../views/view-employee-list/view-employee-list.component';
import { LoginComponent } from '../views/login/login.component';
import { AdminComponent } from '../views/admin/admin.component';
import { EmployeesListComponent } from '../views/admin/EmployeesList/employeesList.component';
import { AddEmployeesComponent } from '../views/admin/EmployeesList/Add/addEmployee.component';
import { DeparmentListComponent } from '../views/admin/DepartmentList/deparment-list.component';
export const routes: Routes = [
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    },
    {
        path: 'login',
        title: 'Login',
        component: LoginComponent
    },
    {
        path: 'viewEmployeeList',
        title: 'Xem Danh Sach Nhan Vien',
        component: ViewEmployeeListComponent
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
                    }
                ] 
            },
            {
                path: 'departmentList',
                title: 'departmentList',
                component: DeparmentListComponent
            }
        ] 
    }
];
