

import { RouterOutlet } from '@angular/router';
import { ViewEmployeeListComponent } from '../manager/view-employee-list/view-employee-list.component';
import { EmployeesListComponent } from "./EmployeesList/employeesList.component";

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
  imports: [RouterOutlet, EmployeesListComponent],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent {
  
}
