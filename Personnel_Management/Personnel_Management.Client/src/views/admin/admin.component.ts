

import { RouterOutlet } from '@angular/router';
import { ViewEmployeeListComponent } from '../manager/view-employee-list/view-employee-list.component';
import { EmployeesListComponent } from "./EmployeesList/employeesList.component";
import { AfterViewInit, Component, inject, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [RouterOutlet,CommonModule],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent {
  isAdminDashboard: boolean = true;
  constructor(private router: Router, private activatedRoute: ActivatedRoute) {
    this.router.events.subscribe(() => {
      // Kiểm tra nếu router link hiện tại là child của 'admin'
      this.isAdminDashboard = this.router.url === '/admin';
    });
  }
}
