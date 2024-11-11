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
  styleUrl: './deparment-list.component.scss'
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
      console.log(this.departmentList); 
      this.dttrigger.next(null);  
    });
  }

  deleteDepartment(phongBanId: number) {
    if (confirm('Bạn có chắc chắn muốn xóa phòng ban này không?')) {
      // Sending the DELETE request to the backend
      this.http.delete(`https://localhost:7182/api/Department/${phongBanId}`).subscribe(
        (response) => {
          // If deletion is successful, refresh the department list and show success message
          console.log('Phòng ban đã bị xóa');
          alert('Xóa phòng ban thành công');
          this.getAllDepartment();  // Refresh the department list
        },
        (error) => {
          // Handle error response
          if (error.status === 400 && error.error?.message) {
            // Show the message from the backend (e.g., "Department has employees and cannot be deleted")
            alert(error.error.message);  // Display the message returned from the API
          } else {
            alert('Phong ban đã có người không thể xóa');
          }
          console.error('Có lỗi khi xóa phòng ban:', error);
        }
      );
    }
  }
  
  
}
