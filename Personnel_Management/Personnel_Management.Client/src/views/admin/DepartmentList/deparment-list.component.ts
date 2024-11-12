import { AfterViewInit, Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
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
  dtoptions: Config = {};
  dttrigger: Subject<any> = new Subject<any>();
  refreshListSubject: Subject<void> = new Subject<void>(); // Subject để thông báo làm mới danh sách

  ngOnInit(): void {
    this.getAllDepartment();

    // Thiết lập tùy chọn DataTables
    this.dtoptions = {
      pagingType: 'full_numbers',
      lengthMenu: [5, 8, 15, 20],
      pageLength: 5
    };

    // Lắng nghe sự kiện từ Subject để làm mới danh sách khi có sự kiện thêm mới
    this.refreshListSubject.subscribe(() => {
      this.getAllDepartment(); // Gọi lại hàm lấy danh sách mới
    });
  }

  getAllDepartment() {
    // Gửi yêu cầu GET để lấy danh sách phòng ban từ API
    this.http.get("https://localhost:7182/api/Department").subscribe((res: any) => {
      this.departmentList = res.$values;
      console.log(this.departmentList);
      this.dttrigger.next(null);  // Kích hoạt DataTables làm mới dữ liệu
    });
  }

  deleteDepartment(phongBanId: number) {
    if (confirm('Bạn có chắc chắn muốn xóa phòng ban này không?')) {
      // Gửi yêu cầu DELETE để xóa phòng ban
      this.http.delete(`https://localhost:7182/api/Department/${phongBanId}`).subscribe(
        (response) => {
          console.log('Phòng ban đã bị xóa');
          alert('Xóa phòng ban thành công');
          this.getAllDepartment();  // Làm mới danh sách sau khi xóa thành công
        },
        (error) => {
          if (error.status === 400 && error.error?.message) {
            alert(error.error.message);  // Thông báo lỗi từ backend
          } else {
            alert('Phòng ban đã có người, không thể xóa');
          }
          console.error('Có lỗi khi xóa phòng ban:', error);
        }
      );
    }
  }
}
