import { AfterViewInit, Component, inject, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent implements OnInit {
  departmentList: any[]= [];
  http = inject(HttpClient);
  dtoptions: Config={};
  dttrigger: Subject<any> = new Subject<any>(); 
  ngOnInit(): void {
    this.getAllDepartment();
    this.dtoptions={
      pagingType: 'full_numbers',
      lengthMenu:[5,8,15,20],
      pageLength: 5
    }
  }
  getAllDepartment() {
    this.http.get("https://localhost:7182/api/Department").subscribe((res:any) =>{
      this.departmentList = res.$values;
      console.log(this.departmentList); 
      this.dttrigger.next(null);  
    })
  }
}
