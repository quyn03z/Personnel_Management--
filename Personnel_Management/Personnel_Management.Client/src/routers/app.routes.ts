import { Routes } from '@angular/router';
import { DashboardComponent } from '../views/dashboard/dashboard.component';

export const routes: Routes = [
    {
        path: '',
        redirectTo: '/dashboard',
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
    },

];
