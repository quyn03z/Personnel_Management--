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
      console.log(this.departmentList); 
      this.dttrigger.next(null);  
    });
  }

  // Hàm xóa phòng ban
  deleteDepartment(phongBanId: number) {
    console.log(`Đang kiểm tra xem phòng ban ${phongBanId} có nhân viên hay không...`);
    
    // Gọi API để kiểm tra nếu phòng ban có nhân viên
    this.http.get<boolean>(`https://localhost:7182/api/Department/${phongBanId}/employees`).subscribe({
      next: (hasEmployees: boolean) => {
        console.log("Kết quả kiểm tra nhân viên:", hasEmployees);
  
        if (hasEmployees) {
          // Nếu có nhân viên, hiển thị cảnh báo và không xóa
          alert("Không thể xóa phòng ban này vì có nhân viên trong đó.");
        } else {
          // Nếu không có nhân viên, tiến hành xóa phòng ban
          console.log(`Không có nhân viên. Đang xóa phòng ban ${phongBanId}...`);
          
          this.http.delete(`https://localhost:7182/api/Department/${phongBanId}`).subscribe({
            next: () => {
              alert("Xóa phòng ban thành công.");
              this.getAllDepartment(); // Cập nhật lại danh sách phòng ban sau khi xóa
            },
            error: (error) => {
              console.error("Lỗi khi xóa phòng ban:", error);
              alert("Đã xảy ra lỗi khi xóa phòng ban.");
            }
          });
        }
      },
      error: (error) => {
        console.error("Lỗi khi kiểm tra nhân viên:", error);
        alert("Đã xảy ra lỗi khi kiểm tra nhân viên.");
      }
    });
  }
  
  
}
