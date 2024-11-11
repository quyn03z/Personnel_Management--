import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = "https://localhost:7182/api";
  private headerCustom = {};

  constructor(private http: HttpClient) {
    this.headerCustom = { headers: { "Authorization": "Bearer " + localStorage.getItem("token") } }
  }


  login(data: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + "/Auth/Login", data);
  }


  sendOtp(data: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/Auth/ForgetPassword`, data, { withCredentials: true });
  }
  
  confirmOtp(data: { email: string; otp: string; token: string }): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(`${this.baseUrl}/Auth/VerifyOtp`, data, { headers });
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
      const userId = decodedToken?.NhanVienId;
      if (!userId) {
        throw new Error('User ID not found in token');
      }
      return this.http.get<any>(`${this.baseUrl}/NhanViens/${userId}`);
    } catch (error) {
      console.error('Error decoding token:', error);
      throw error;
    }
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
    const decodedPayload = atob(payload); // Decode base64
    return JSON.parse(decodedPayload); // Parse JSON to return as an object
  } catch (error) {
    console.error('Failed to decode JWT:', error);
    return null;
  }
}

