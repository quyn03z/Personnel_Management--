import { Routes } from '@angular/router';
import { DashboardComponent } from '../views/dashboard/dashboard.component';
import { ViewEmployeeListComponent } from '../views/view-employee-list/view-employee-list.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
    },
    {
        path: 'dashboard',
        title: 'Dashboard',
        component: DashboardComponent,
        // canActivate: [AuthGuard, RoleGuard],
        // data:{
        //   roles: ['Patient', 'Doctor']
        // }
        children: [
            {
                path: '',
                redirectTo: 'viewEmployeeList',
                pathMatch: 'full'
            },
            {
                path: 'viewEmployeeList',
                title: 'Xem Danh Sach Nhan Vien',
                component: ViewEmployeeListComponent
            }
        ]
    },

];
