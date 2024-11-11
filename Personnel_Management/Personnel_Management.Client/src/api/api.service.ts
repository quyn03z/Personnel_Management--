import { HttpClient } from '@angular/common/http';
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
  
  
}
