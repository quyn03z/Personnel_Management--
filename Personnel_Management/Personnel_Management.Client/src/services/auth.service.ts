import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { Router } from '@angular/router';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private apiService: ApiService, private router: Router) { }


  isLoggedIn(): boolean {
    const token = localStorage.getItem("token");
    if (token) {
      const tokenData = this.parseJwt(token);
      const currentTime = Math.floor(Date.now() / 1000);
      if (tokenData && tokenData.exp > currentTime) {
        return true;
      } else {
        this.logout();
      }
    }
    return false;
  }

  login(email: string, matKhau: string) {
    const payload = { email, matKhau };
    return this.apiService.login(payload).pipe(
      tap(response => {
        if (response.token) {
          localStorage.setItem("token", response.token);
        } else {
          console.error('Không nhận được token hợp lệ từ phản hồi.');
        }
      })
    );
  }

  logout(): void {
    localStorage.clear();
  }

  getRoles() {
    const token = localStorage.getItem('token');
    const decodedToken = this.parseJwt(token ?? "");
    return decodedToken?.role;
  }

  parseJwt(token: string) {
    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      const jsonPayload = decodeURIComponent(atob(base64).split('').map(c =>
        '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
      ).join(''));
      return JSON.parse(jsonPayload);
    } catch (error) {
      console.error("Token không hợp lệ hoặc không thể giải mã:", error);
      return null;
    }
  }


  sendMailOTP(data: any) {
    return this.apiService.sendOtp(data);
  }


  confirmOTP(data: any) {
    return this.apiService.confirmOtp(data);
  }

  changeNewPassWord(data: any) {
    return this.apiService.changePassWord(data);
  }

  getNhanVien() {
    return this.apiService.getUserById();
  }


}
