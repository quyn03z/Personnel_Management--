import { AfterViewInit, Component, inject, OnInit } from '@angular/core';

import { CommonModule, DatePipe } from '@angular/common';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';
import { ApiService } from '../../../api/api.service';

@Component({
  selector: 'app-view-employee-list',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, DataTablesModule, DatePipe],
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

  dtoptions: Config = {};
  dttrigger: Subject<any> = new Subject<any>();



  ngOnInit(): void {
    this.getAllEmployee();
    this.dtoptions = {
      pagingType: 'full_numbers',
      lengthMenu: [5, 8, 15, 20],
      pageLength: 5
    }
  }

  getAllEmployee(): void {
    this.http.get("https://localhost:7182/api/NhanViens/GetAllManagerFunction").subscribe((res: any) => {

      this.employeeList = res.$values.map((employee: any) => ({
        ...employee,
        ngaySinh: this.datePipe.transform(employee.ngaySinh, 'yyyy-MM-dd')
      }));
      console.log(this.employeeList);
      this.dttrigger.next(null);
    });
  }

  NavigateToEmployeeDetail(nhanVienId: any) {
    this.router.navigate(['/viewEmployeeList', nhanVienId]);
  }

}
