import { AfterViewInit, Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-deparment-list',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, DataTablesModule],
  templateUrl: './deparment-list.component.html',
  styleUrls: ['./deparment-list.component.scss']
})
export class DeparmentListComponent implements OnInit {
  departmentList: any[] = [];
  http = inject(HttpClient);
  dtoptions: Config = {};
  dttrigger: Subject<any> = new Subject<any>(); 

  ngOnInit(): void {
    this.getAllDepartment();
    this.dtoptions = {
      pagingType: 'full_numbers',
      lengthMenu: [5, 8, 15, 20],
      pageLength: 5
    };
  }

  getAllDepartment() {
    this.http.get("https://localhost:7182/api/Department").subscribe((res: any) => {
      this.departmentList = res.$values;
      this.dttrigger.next(null);  
    });
  }

  deleteDepartment(departmentId: number) {
    // Check if there are employees in the department
    this.http.get<any[]>(`https://localhost:7182/api/Department/${departmentId}/employees`).subscribe((employees) => {
      if (employees.length > 0) {
        alert('Cannot delete department with employees assigned.');
      } else {
        // Confirm deletion
        if (confirm('Are you sure you want to delete this department?')) {
          this.http.delete(`https://localhost:7182/api/Department/${departmentId}`).subscribe(() => {
            alert('Department deleted successfully.');
            // Refresh the department list
            this.getAllDepartment();
          }, error => {
            alert('Error occurred while deleting the department.');
          });
        }
      }
    });
  }
  
}
