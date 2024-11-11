import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-create-thuong-phat',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './create-thuong-phat.component.html',
  styleUrl: './create-thuong-phat.component.scss'
})
export class CreateThuongPhatComponent implements OnInit {
  employee: any;
  nhanVienId: any;
  addThuongPhat: any ={
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
  }

  getEmployeeById(){
    this.nhanVienId = this.activatedRoute.snapshot.paramMap.get('nhanVienId');
    if(this.nhanVienId){
      this.http.get('https://localhost:7182/api/NhanViens/GetByIdManagerFunction?id='+this.nhanVienId).subscribe((res: any) =>{
        if (res){
          this.employee = res;
          this.addThuongPhat.nhanVienId = res.nhanVienId;
        }
      })
    }
  }

  btnAdd(){
    this.http.post('https://localhost:7182/api/ThuongPhat/AddThuongPhat?nhanVienId='+this.nhanVienId, this.addThuongPhat).subscribe((res: any) =>{
      if(res){
        alert("Thêm thành công");
        this.router.navigate(['/viewEmployeeDetail', this.nhanVienId]);
      } else
        alert("Thêm thất bại");
    })
  }
  btnBack(){
    this.router.navigate(['/viewEmployeeDetail', this.nhanVienId]);
  }
}
