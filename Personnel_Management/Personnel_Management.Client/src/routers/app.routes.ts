import { Routes } from '@angular/router';
import { ViewEmployeeListComponent } from '../views/manager/view-employee-list/view-employee-list.component';
import { LoginComponent } from '../views/login/login.component';
import { AdminComponent } from '../views/admin/admin.component';
import { ViewEmployeeDetailComponent } from '../views/view-employee-detail/view-employee-detail.component';
import { CreateThuongPhatComponent } from '../views/ThuongPhatService/create-thuong-phat/create-thuong-phat.component';
import { UpdateThuongPhatComponent } from '../views/ThuongPhatService/update-thuong-phat/update-thuong-phat.component';
import { EmployeesListComponent } from '../views/admin/EmployeesList/employeesList.component';
import { DeparmentListComponent } from '../views/admin/DepartmentList/deparment-list.component';
import { AttendanceReportComponent } from '../views/manager/attendance-report/attendance-report.component';
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
        path: 'attendanceReport/:nhanVienId',
        title: 'Xem điểm danh nhân viên',
        component: AttendanceReportComponent
    },
    {
        path: 'admin',
        title: 'admin',
        component: AdminComponent,
        children : [
            {
                path: 'employeesList',
                title: 'employeesList',
                component: EmployeesListComponent
            },
            {
                path: 'departmentList',
                title: 'departmentList',
                component: DeparmentListComponent
            }
        ] 
    }
    
];
