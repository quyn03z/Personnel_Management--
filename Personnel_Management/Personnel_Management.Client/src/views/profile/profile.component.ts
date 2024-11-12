import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  avatarBase64: string | null = null;

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
    this.setRoleIdFromToken();
    const storedRoleId = localStorage.getItem('roleId') || '0';
    this.nhanVienProfileObj.roleId = storedRoleId;
    console.log('Assigned roleId from localStorage to profile object:', this.nhanVienProfileObj.roleId);
  }
  profileImageUrl: string | ArrayBuffer | null = '';

  setRoleIdFromToken() {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decodedToken: any = jwt_decode(token);
        console.log('Decoded Token:', decodedToken); // Check token structure

        // Extract RoleId and save it to localStorage
        if (decodedToken && decodedToken.RoleId) {
          localStorage.setItem('roleId', decodedToken.RoleId); // Store RoleId separately
          console.log('RoleId saved to localStorage:', decodedToken.RoleId);
        } else {
          console.warn('RoleId not found in token');
          localStorage.setItem('roleId', '0'); // Default if RoleId is missing
        }

      } catch (error) {
        console.error('Error decoding token:', error);
      }
    } else {
      console.error('Token not found');
    }
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
          avatar: nhanVien.avatar && !nhanVien.avatar.startsWith('assets/')
            ? `assets/${nhanVien.avatar}`
            : nhanVien.avatar
        };
      },
      (error) => {
        console.error('Error fetching profile data', error);
      }
    );
  }




  onUpdateProfile() {
    if (!this.nhanVienProfileObj.hoTen || this.nhanVienProfileObj.hoTen.trim() === '') {
      alert('Vui lòng nhập Họ và Tên.');
      return;
    }
    const updateData = {
      hoTen: this.nhanVienProfileObj.hoTen,
      ngaySinh: this.nhanVienProfileObj.ngaySinh,
      diaChi: this.nhanVienProfileObj.diaChi,
      soDienThoai: this.nhanVienProfileObj.soDienThoai,
      email: this.nhanVienProfileObj.email,
      roleId: this.nhanVienProfileObj.roleId,
      avatar: this.nhanVienProfileObj.avatar
    };
    console.log('Sending update data:', updateData);

    this.authService.updateNewProfile(updateData).subscribe({
      next: (response) => {
        alert('Profile updated successfully!');
        console.log('Update response:', response);
      },
      error: (error) => {
        if (error.status === 400) {
          const errorMessage = error.error.message || "Đã xảy ra lỗi khi cập nhật thông tin.";
          alert(errorMessage);
        } else {
          console.error('Error updating profile:', error);
          alert('Failed to update profile. Please try again.');
        }
      }
    });

  }


  previewImage(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.avatarBase64 = e.target.result; // Store base64 string for preview
      };
      reader.readAsDataURL(file);
      this.nhanVienProfileObj.avatar = file.name; // Set only the filename for the avatar
    }
  }


  changePasswordObj = {
    oldPassword: '',
    newPassword: '',
  };


  onChangePassWordProfile() {
    const changePassWordProfile = {
      oldPassword: this.changePasswordObj.oldPassword,
      newPassword: this.changePasswordObj.newPassword,
    };
    console.log('Sending update data:', changePassWordProfile);
    this.authService.changePassWordProfileNew(changePassWordProfile).subscribe({
      next: (response) => {
        alert('Password updated successfully!');
        console.log('Update response:', response);
      },
      error: (error) => {
        console.error('Error updating password:', error);
        console.log('Error details:', error.error);

        if (error.status === 400) {
          alert('Old password is incorrect. Please try again.');
        } else if (error.status === 500) {
          alert('Server error. Please try again later.');
        } else {
          alert('Failed to update password. Please try again.');
        }
      }
    });
  }

}


function jwt_decode(token: string): any {
  try {
    // Split the token into its parts
    const parts = token.split('.');
    if (parts.length !== 3) {
      throw new Error('Invalid JWT token format');
    }

    // Decode the payload (second part of the token)
    const payload = parts[1];
    const decodedPayload = atob(payload);
    return JSON.parse(decodedPayload);
  } catch (error) {
    console.error('Failed to decode JWT:', error);
    return null;
  }
}
