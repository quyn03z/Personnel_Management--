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
        console.log('Profile Data:', data);  
        this.nhanVienProfileObj = {
          hoTen: data.hoTen,
          email: data.email,
          ngaySinh: data.ngaySinh ? data.ngaySinh.substring(0, 10) : '',
          diaChi: data.diaChi,
          roleId: data.roleId,
          matKhau: '', 
          phongBanId: data.phongBanId,
          soDienThoai: data.soDienThoai,
          phongBanName: data.phongBan ? data.phongBan.tenPhongBan : '',  
          roleName: data.role ? data.role.tenRole : '',
          avatar: data.avatar
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
