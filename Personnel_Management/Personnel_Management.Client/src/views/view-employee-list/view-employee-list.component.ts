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
  templateUrl: './view-employee-list.component.html',
  styleUrls: ['./view-employee-list.component.scss']
})
export class ViewEmployeeListComponent implements  OnInit {
  employeeList: any[] = [];
  http = inject(HttpClient);
  dtoptions: Config={};
  dttrigger: Subject<any> = new Subject<any>();

  ngOnInit(): void {
    this.getAllEmployee();
    this.dtoptions ={
      pagingType: 'full_numbers',
      lengthMenu:[5,8,15,20],
      pageLength: 5
    }
  }

  getAllEmployee(): void {
    this.http.get("https://localhost:7182/api/NhanViens/GetAllManagerFunction").subscribe((res: any) => {
      
      this.employeeList = res.$values;
      console.log(this.employeeList);
      this.dttrigger.next(null);
    });
  }

}
