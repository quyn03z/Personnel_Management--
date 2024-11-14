import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-update-thuong-phat',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './update-thuong-phat.component.html',
  styleUrls: ['./update-thuong-phat.component.scss']
})
export class UpdateThuongPhatComponent implements OnInit {
  employee: any;
  nhanVienId: any;
  thuongPhatId: any;
  phongBanId: any;
  updateThuongPhat: any = {
    "thuongPhatId": 0,
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
    this.getThuongPhat();
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
            this.updateThuongPhat.nhanVienId = res.nhanVienId;
          }
        });
    }
  }

  getThuongPhat() {
    this.thuongPhatId = this.activatedRoute.snapshot.paramMap.get('thuongPhatId');
    if (this.thuongPhatId) {
      this.http.get(`https://localhost:7182/api/ThuongPhat/GetThuongPhatByThuongPhatId?thuongPhatId=${this.thuongPhatId}`)
        .pipe(
          catchError(error => {
            console.error('Error fetching ThuongPhat:', error);
            alert("Không tìm thấy thông tin thưởng phạt.");
            this.router.navigate(['/viewEmployeeDetail', this.nhanVienId]);
            return of(null);
          })
        )
        .subscribe((res: any) => {
          if (res) {
            this.updateThuongPhat = {
              thuongPhatId: res.thuongPhatId || "",
              nhanVienId: res.nhanVienId || "",
              ngay: new Date(res.ngay).toISOString().split('T')[0],
              soTien: res.soTien || "",
              loai: res.loai || "",
              ghiChu: res.ghiChu || ""
            };
          }
        });
    }
  }

  btnUpdate() {
    if (!this.isFormValid()) {
      alert("Vui lòng điền đầy đủ thông tin trước khi cập nhật.");
      return;
    }

    this.http.put(`https://localhost:7182/api/ThuongPhat/UpdateThuongPhat?thuongPhatId=${this.thuongPhatId}`, this.updateThuongPhat)
      .pipe(
        catchError(error => {
          console.error('Error updating ThuongPhat:', error);
          alert("Cập nhật thất bại, vui lòng thử lại sau.");
          return of(null);
        })
      )
      .subscribe((res: any) => {
        if (res === true) {
          alert("Cập nhật thành công");
          this.router.navigate(['/viewEmployeeDetail', this.nhanVienId]);
        }
      });
  }

  btnBack() {
    this.router.navigate(['/viewEmployeeDetail', this.nhanVienId]);
  }

  private isFormValid(): boolean {
    return this.updateThuongPhat.ngay && this.updateThuongPhat.soTien && this.updateThuongPhat.loai && this.updateThuongPhat.ghiChu;
  }
}
