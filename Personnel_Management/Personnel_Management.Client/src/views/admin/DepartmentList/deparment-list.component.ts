import { AfterViewInit, Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-deparment-list',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, DataTablesModule, RouterOutlet],
  templateUrl: './deparment-list.component.html',
  styleUrls: ['./deparment-list.component.scss']
})
export class DeparmentListComponent implements OnInit {
  departmentList: any[] = [];
  http = inject(HttpClient);
  router = inject(Router); // Inject Router sử dụng cú pháp mới
  dtoptions: Config = {};
  dttrigger: Subject<any> = new Subject<any>();
  refreshListSubject: Subject<void> = new Subject<void>();

  ngOnInit(): void {
    this.getAllDepartment();

    this.dtoptions = {
      pagingType: 'full_numbers',
      lengthMenu: [5, 8, 15, 20],
      pageLength: 5
    };

    this.refreshListSubject.subscribe(() => {
      this.getAllDepartment();
    });
  }

  getAllDepartment() {
    this.http.get("https://localhost:7182/api/Department").subscribe((res: any) => {
      this.departmentList = res.$values;
      this.dttrigger.next(null);
    });
  }

  deleteDepartment(phongBanId: number) {
    if (confirm('Bạn có chắc chắn muốn xóa phòng ban này không?')) {
      this.http.delete(`https://localhost:7182/api/Department/${phongBanId}`).subscribe(
        () => {
          alert('Xóa phòng ban thành công');
          this.getAllDepartment();
        },
        (error) => {
          if (error.status === 400 && error.error?.message) {
            alert(error.error.message);
          } else {
            alert('Phòng ban đã có người, không thể xóa');
          }
        }
      );
    }
  }

  editDepartment(phongBanId: number) {
    this.router.navigate(['/admin/departmentList/edit-department', phongBanId]);
  }
}
