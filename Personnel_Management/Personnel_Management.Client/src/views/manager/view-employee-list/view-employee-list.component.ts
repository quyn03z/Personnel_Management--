import { AfterViewInit, Component, inject, OnInit } from '@angular/core';

import { CommonModule, DatePipe } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';
import { ApiService } from '../../../api/api.service';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-view-employee-list',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, DataTablesModule],
  providers: [DatePipe], // Thêm DatePipe vào providers
  templateUrl: './view-employee-list.component.html',
  styleUrls: ['./view-employee-list.component.scss']
})
export class ViewEmployeeListComponent implements OnInit {
  employeeList: any[] = [];
  http = inject(HttpClient);
  router = inject(Router);
  datePipe = inject(DatePipe);
  apiService = inject(ApiService);
  phongBanId: any;
  phongBan: any;
  dtoptions: Config = {};
  dttrigger: Subject<any> = new Subject<any>();



  ngOnInit(): void {
    this.phongBanId = localStorage.getItem('phongBanId');
    this.getPhongBan();
    this.getAllEmployee();
    this.dtoptions = {
      pagingType: 'full_numbers',
      lengthMenu: [5, 8, 15, 20],
      pageLength: 5
    }
  }

 

 



  getAllEmployee(): void {
    this.http.get("https://localhost:7182/api/NhanViens/GetAllManagerFunction?phongBanId=" + this.phongBanId).subscribe((res: any) => {

      this.employeeList = res.$values.map((employee: any) => ({
        ...employee,
        ngaySinh: this.datePipe.transform(employee.ngaySinh, 'yyyy-MM-dd')
      }));
      console.log(this.employeeList);
      this.dttrigger.next(null);
    });
  }

  getPhongBan() {
    if (this.phongBanId) {
      this.http.get(`https://localhost:7182/api/Department/${this.phongBanId}`).subscribe((res: any) => {
        this.phongBan = res;
      })
    }

  }

  getRoleName(roleId: number): string {
    switch (roleId) {
      case 1:
        return 'Admin';
      case 2:
        return 'Manager';
      case 3:
        return 'Employee';
      default:
        return 'Unknown';
    }
  }

  NavigateToEmployeeDetail(nhanVienId: any) {
    this.router.navigate(['/viewEmployeeList', nhanVienId]);
  }

}
