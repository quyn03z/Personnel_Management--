import { CurrencyPipe, DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';
@Component({
  selector: 'app-view-employee-detail',
  standalone: true,
  imports: [DataTablesModule, RouterLink, RouterLinkActive, CurrencyPipe],
  providers: [DatePipe, CurrencyPipe], // Thêm DatePipe vào providers
  templateUrl: './view-employee-detail.component.html',
  styleUrl: './view-employee-detail.component.scss'
})
export class ViewEmployeeDetailComponent implements OnInit {
  employee!: any;
  employeeList: any[] = [];
  nhanVienId: any;
  thuongPhatList!: any[];
  luong: any;
  today: any;

  http = inject(HttpClient);
  router = inject(Router);
  datePipe = inject(DatePipe)
  activatedRoute = inject(ActivatedRoute);
  dtOptions: Config = {};
  dtTrigger: Subject<any> = new Subject<any>();

  check = false;

  ngOnInit(): void {
    const time = new Date();
    this.today = this.datePipe.transform(time, 'dd/MM/yyyy') || '';

    this.getEmployeeById();
    this.getLuong();
    // this.getAllEmployee();
    this.getListThuongPhat();
    this.dtOptions = {
      pagingType: 'simple_numbers',
      pageLength: 3,
      lengthChange: false
    }
  }

  getEmployeeById() {
    this.nhanVienId = this.activatedRoute.snapshot.paramMap.get('nhanVienId');
    if (this.nhanVienId) {
      this.http.get('https://localhost:7182/api/NhanViens/GetByIdManagerFunction?id=' + this.nhanVienId).subscribe((res: any) => {
        if (res) {
          this.employee = {
            ...res,
            ngaySinh: this.datePipe.transform(res.ngaySinh, 'yyyy-MM-dd')
        };
        }
      })
    }
  }

  getListThuongPhat() {
    if (this.nhanVienId) {
      this.http.get('https://localhost:7182/api/ThuongPhat/GetAllThuongPhat?nhanVienId=' + this.nhanVienId).subscribe((res: any) => {
        if (res) {
          this.thuongPhatList = res.danhSachThuongPhat.$values;
          this.dtTrigger.next(null);
        }
      })
    }
  }

  getLuong() {
    if (this.nhanVienId) {
      this.http.get(`https://localhost:7182/api/Luong/GetSalaryEmployee?nhanVienId=${this.nhanVienId}`).subscribe((res: any) => {
        this.luong = res;
      });
    }
  }

  btnDelete(thuongPhatId: any) {
    const isDelete = confirm("Xác nhận muốn xóa thưởng phạt này của nhân viên");
    if (isDelete) {
      this.http.delete('https://localhost:7182/api/ThuongPhat/DeleteThuongPhatById?thuongPhatId=' + thuongPhatId).subscribe((res: any) => {
        if (res) {
          alert("Xóa thành công");
          this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigate([
              `/viewEmployeeDetail/${this.nhanVienId}`
            ]);
          });
        }
      })
    } else {
      alert("Xóa không thành công");
    }
  }

  btnDiemDanh(){
    this.router.navigateByUrl(`attendanceReport/${this.nhanVienId}`);
  }
}
