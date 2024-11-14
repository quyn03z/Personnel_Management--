import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})

export class NavbarComponent implements OnInit {


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
        console.log('Profile Data:', data);
        const nhanVien = data.nhanVienDTO;
        this.nhanVienProfileObj = {
          hoTen: nhanVien.hoTen,
          email: nhanVien.email,
          ngaySinh: nhanVien.ngaySinh ? nhanVien.ngaySinh.substring(0, 10) : '',
          diaChi: nhanVien.diaChi,
          roleId: this.nhanVienProfileObj.roleId,
          matKhau: '',
          phongBanId: nhanVien.phongBanId,
          soDienThoai: nhanVien.soDienThoai,
          phongBanName: nhanVien.phongBanName || 'No Department',
          roleName: nhanVien.roleName || 'No Role',
          avatar: nhanVien.avatar && !nhanVien.avatar.startsWith('assets\\')
            ? `assets/${nhanVien.avatar}`
            : nhanVien.avatar
        };
      },
      (error) => {
        console.error('Error fetching profile data', error);
      }
    );
  }

}
