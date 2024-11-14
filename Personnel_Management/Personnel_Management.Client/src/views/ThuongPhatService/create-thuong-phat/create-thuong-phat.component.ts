import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-create-thuong-phat',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './create-thuong-phat.component.html',
  styleUrls: ['./create-thuong-phat.component.scss']
})
export class CreateThuongPhatComponent implements OnInit {
  employee: any;
  nhanVienId: any;
  phongBanId: any;
  addThuongPhat: any = {
    "nhanVienId": 0,
    "ngay": "",
    "soTien": 0,
    "loai": "",
    "ghiChu": ""
  };

  activatedRoute = inject(ActivatedRoute);
  http = inject(HttpClient);
  router = inject(Router);

  ngOnInit(): void {
    this.phongBanId = localStorage.getItem('phongBanId');
    this.getEmployeeById();
  }

  getEmployeeById() {
    this.nhanVienId = this.activatedRoute.snapshot.paramMap.get('nhanVienId');
    if (this.nhanVienId) {
      this.http.get(`https://localhost:7182/api/NhanViens/GetByIdManagerFunction?id=${this.nhanVienId}&phongBanId=${this.phongBanId}`)
        .pipe(
          catchError(error => {
            console.error('Error fetching employee:', error);
            alert("Không tìm thấy nhân viên.");
            this.router.navigate(['/employeeList']);
            return of(null);
          })
        )
        .subscribe((res: any) => {
          if (res) {
            this.employee = res;
            this.addThuongPhat.nhanVienId = res.nhanVienId;
          }
        });
    }
  }

  btnAdd() {
    if (!this.isFormValid()) {
      alert("Vui lòng điền đầy đủ thông tin trước khi thêm.");
      return;
    }

    this.http.post(`https://localhost:7182/api/ThuongPhat/AddThuongPhat?nhanVienId=${this.nhanVienId}`, this.addThuongPhat)
      .pipe(
        catchError(error => {
          console.error('Error adding ThuongPhat:', error);
          alert("Thêm thất bại, vui lòng thử lại sau.");
          return of(null);
        })
      )
      .subscribe((res: any) => {
        if (res) {
          alert("Thêm thành công");
          this.router.navigate(['/viewEmployeeDetail', this.nhanVienId]);
        }
      });
  }

  btnBack() {
    this.router.navigate(['/viewEmployeeDetail', this.nhanVienId]);
  }

  private isFormValid(): boolean {
    return this.addThuongPhat.ngay && this.addThuongPhat.soTien && this.addThuongPhat.loai && this.addThuongPhat.ghiChu;
  }
}
