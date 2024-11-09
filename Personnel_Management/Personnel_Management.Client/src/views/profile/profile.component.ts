import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  nhanVienProfileObj = {
    hoTen: '',
    email: '',
    ngaySinh: '',
    diaChi: '',
    roleId: '',
    matKhau: '',
    phongBanId:'',
    soDienThoai: '',
    phongBanName: '',
    roleName: ''
  };

  constructor(private router: Router, private authService: AuthService){

  }
  ngOnInit(): void {
   this. getProfileNhanVien();
  }


  getProfileNhanVien() {
    this.authService.getNhanVien().subscribe(
      (data: any) => {
        const nhanVien = data.nhanVienDTO; // Lấy đối tượng nhanVienDTO từ API

        const ngaySinh = nhanVien.ngaySinh ? nhanVien.ngaySinh.substring(0, 10) : '';
        // Gán các thuộc tính từ nhanVienDTO vào nhanVienProfileObj
        this.nhanVienProfileObj = {
          hoTen: nhanVien.hoTen,
          email: nhanVien.email,
          ngaySinh: ngaySinh,
          diaChi: nhanVien.diaChi,
          roleId: nhanVien.roleId,
          phongBanId: nhanVien.phongBanId,
          soDienThoai: nhanVien.soDienThoai,
          phongBanName: nhanVien.phongBanName,
          roleName: nhanVien.roleName,
          matKhau: '' // giữ trống nếu không cần
        };
      },
      (error) => {
        console.error('Error fetching profile data', error);
      }
    );
  }



}
