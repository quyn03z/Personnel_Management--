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
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit{
  
  nhanVienProfileObj = {
    hoTen: '',
    email: '',
    ngaySinh: '',
    diaChi: '',
    roleId: '',
    matKhau: '',
    phongBanId: '',
    soDienThoai: '',
    phongBanName: '',
    avatar: '',
    roleName: ''
  };

  constructor(private router: Router, private authService: AuthService) {

  }
  ngOnInit(): void {
    this.getProfileNhanVien();
  }


  getProfileNhanVien() {
    this.authService.getNhanVien().subscribe(
      (data: any) => {
        const nhanVien = data.nhanVienDTO;

        const ngaySinh = nhanVien.ngaySinh ? nhanVien.ngaySinh.substring(0, 10) : '';
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
          avatar: nhanVien.avatar,
          matKhau: ''
        };
      },
      (error) => {
        console.error('Error fetching profile data', error);
      }
    );
  }


  onUpdateProfile() {

  }
}
