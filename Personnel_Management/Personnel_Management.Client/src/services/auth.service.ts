import { Injectable } from '@angular/core';
import { ApiService } from '../api/api.service';
import { Router } from '@angular/router';
import { BehaviorSubject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private apiService: ApiService, private router: Router) {
    setInterval(() => {
      this.isLoggedIn();
    }, 30000);
  }


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
          const token = localStorage.getItem('token');
          if (token) {
            const decodedToken: any = jwt_decode(token);
            const PhongBanId = decodedToken?.PhongBanId;
            const NhanVienId = decodedToken?.NhanVienId;
            localStorage.setItem("phongBanId", PhongBanId);
            localStorage.setItem("NhanVienId", NhanVienId);
          } else {
            console.error('Token not found in localStorage.');
          }
        } else {
          console.error('Không nhận được token hợp lệ từ phản hồi.');
        }
      })
    );
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/login']);
  }

  getRoles(): string[] {
    const token = localStorage.getItem('token');
    const decodedToken = this.parseJwt(token ?? "");
    const roles = decodedToken?.role || [];
    return Array.isArray(roles) ? roles : [roles];
  }

  setRoles(roles: string[]): void {
    this.rolesSubject.next(roles);
  }
  
  private rolesSubject = new BehaviorSubject<string[]>([]);
  roles$ = this.rolesSubject.asObservable();

  loadRolesFromToken(): void {
    const token = localStorage.getItem('token');
    const decodedToken = this.parseJwt(token ?? "");
    const roles = decodedToken?.role || [];
    this.setRoles(Array.isArray(roles) ? roles : [roles]);
  }

  hasRole(requiredRoles: string[]): boolean {
    const userRoles = this.getRoles();
    return requiredRoles.some(role => userRoles.includes(role));
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

  updateNewProfile(data: any) {
    return this.apiService.updateProfile(data);
  }

  changePassWordProfileNew(data: any) {
    return this.apiService.changePassWordProfile(data);
  }


  getDiemDanhNhanVien(thang: number, nam: number) {
    return this.apiService.getAllDiemDanh(thang, nam);
  }


  diemDanhNhanVien(data: any) {
    return this.apiService.diemDanhAPI(data);
  }

  checkNgayDiemDanh(ngay: number, thang: number, nam: number) {
    return this.apiService.checkNgayDiemDanhAPI(ngay, thang, nam);
  }

  upLoadPhoto(file: File) {
    return this.apiService.upLoadFileAPI(file);
  }


}


function jwt_decode(token: string): any {
  try {

    const parts = token.split('.');
    if (parts.length !== 3) {
      throw new Error('Invalid JWT token format');
    }
    const payload = parts[1];
    const decodedPayload = atob(payload);
    return JSON.parse(decodedPayload);
  } catch (error) {
    console.error('Failed to decode JWT:', error);
    return null;
  }
}