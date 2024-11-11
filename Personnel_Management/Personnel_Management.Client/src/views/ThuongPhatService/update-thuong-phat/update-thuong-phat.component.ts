import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-update-thuong-phat',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './update-thuong-phat.component.html',
  styleUrl: './update-thuong-phat.component.scss'
})
export class UpdateThuongPhatComponent {
  employee: any;
  nhanVienId: any;
  thuongPhatId: any;
  updateThuongPhat: any ={
    "thuongPhatId": 0,
    "nhanVienId": 0,
    "ngay": "",
    "soTien": 0,
    "loai": "",
    "ghiChu": ""

  }

  activatedRoute = inject(ActivatedRoute);
  http = inject(HttpClient);
  router = inject(Router)

  ngOnInit(): void {
    this.getEmployeeById();
    this.getThuongPhat();
  }

  getEmployeeById(){
    this.nhanVienId = this.activatedRoute.snapshot.paramMap.get('nhanVienId');
    if(this.nhanVienId){
      this.http.get('https://localhost:7182/api/NhanViens/GetByIdManagerFunction?id='+this.nhanVienId).subscribe((res: any) =>{
        if (res){
          this.employee = res;
          this.updateThuongPhat.nhanVienId = res.nhanVienId;
        }
      })
    }
  }

  getThuongPhat(){
    this.thuongPhatId = this.activatedRoute.snapshot.paramMap.get('thuongPhatId');
    if(this.thuongPhatId){
      this.http.get('https://localhost:7182/api/ThuongPhat/GetThuongPhatByThuongPhatId?thuongPhatId='+this.thuongPhatId).subscribe((res: any) =>{
        if(res){
          this.updateThuongPhat = {
            thuongPhatId: res.thuongPhatId || "",
            nhanVienId: res.nhanVienId || "",
            ngay: new Date(res.ngay).toISOString().split('T')[0],
            soTien: res.soTien || "",
            loai: res.loai || "",
            ghiChu: res.ghiChu || ""

          }

        }
      })
    }
  }

  btnUpdate(){
    this.http.put('https://localhost:7182/api/ThuongPhat/UpdateThuongPhat?thuongPhatId='+this.thuongPhatId, this.updateThuongPhat).subscribe((res: any) =>{
      if(res == true){
        alert("Cập nhật thành công");
        this.router.navigate(['/viewEmployeeDetail', this.nhanVienId]);
      } else
        alert("Cập nhật thất bại");
    })
  }
  btnBack(){
    this.router.navigate(['/viewEmployeeDetail', this.nhanVienId]);
  }
}
