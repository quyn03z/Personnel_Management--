import { AfterViewInit, Component, inject, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-view-employee-list',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, DataTablesModule],
  templateUrl: './employeesList.component.html',
  styleUrls: ['./employeesList.component.scss']
})
export class EmployeesListComponent implements  OnInit {
  employeeList: any[] = [];
  http = inject(HttpClient);
  dtoptions: Config={};
  dttrigger: Subject<any> = new Subject<any>();

  ngOnInit(): void {
    this.getAllEmployee();
    this.dtoptions ={
      pagingType: 'full_numbers',
      lengthMenu:[5,8,15,20],
      pageLength: 10
    }
  }

  getAllEmployee(): void {
    this.http.get("https://localhost:7182/api/NhanViens").subscribe((res: any) => {
      
      this.employeeList = res.$values;
      console.log(this.employeeList);
      this.dttrigger.next(null);
    });
    
  }
  getRoleName(roleId: number): string {
    switch (roleId) {
        case 1:
            return 'Employee';
        case 2:
            return 'Manager';
        case 3:
            return 'Admin';
        default:
            return 'Unknown';
    }
}

}
