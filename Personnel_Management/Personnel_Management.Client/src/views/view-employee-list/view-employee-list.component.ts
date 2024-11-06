import { AfterViewInit, Component } from '@angular/core';
import $ from 'jquery';
import 'datatables.net';
import 'datatables.net-bs4';
@Component({
  selector: 'app-view-employee-list',
  standalone: true,
  imports: [],
  templateUrl: './view-employee-list.component.html',
  styleUrl: './view-employee-list.component.scss'
})
export class ViewEmployeeListComponent implements AfterViewInit {
  ngAfterViewInit(): void {
    // Khởi tạo DataTable
    ($('#dataTable') as any).DataTable();
  }
}
