import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { HttpClientModule } from '@angular/common/http';
import jwt_decode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl = "http://localhost:5008/api";
  private headerCustom = {};


  constructor(private http: HttpClient) {
    this.headerCustom = { headers: { "Authorization": "Bearer " + localStorage.getItem("token") } }
  }

  login(data: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + "/Auth/Login", data);
  }

  createNhanVien(data: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + "/Auth/CreateNhanVien", data);
  }

  // Angular service
  sendOtp(data: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/Auth/ForgetPassword`, data, { withCredentials: true });
  }

  confirmOtp(data: { email: string; otp: string }): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(`${this.baseUrl}/Auth/VerifyOtp`, data, { headers, withCredentials: true });
  }

  changePassWord(data: any):Observable<any>{
    return this.http.post<any>(this.baseUrl + "/Auth/ChangePassword", data);
  }


  getUserById() {
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('Token not found');
    }
    try {
      const decodedToken: any = jwt_decode(token); 
      const userId = decodedToken?.nameid;
      if (!userId) {
        throw new Error('User ID not found in token');
      }
      return this.http.get<any>(`${this.baseUrl}/NhanVien/${userId}`);
    } catch (error) {
      console.error('Error decoding token:', error);
      throw error;
    }
  }

 
  updateProfile(data: any, id: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/NhanVien/updateProfileEmployee/${id}`, data);
  }


  

}
