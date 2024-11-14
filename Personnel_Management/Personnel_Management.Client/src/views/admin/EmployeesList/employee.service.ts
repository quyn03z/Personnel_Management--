import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


  export interface Employee {
    nhanVienId: number;
    hoTen: string;
    ngaySinh: string;
    diaChi: string;
    soDienThoai: string;
    email: string;
    roleId: number;
    phongBanId: number;
    avatar: string | null;
    matkhau:string;
  }
  @Injectable({
    providedIn: 'root'
  })
  export class EmployeeService {

    private apiUrl = 'https://localhost:7182/api/NhanViens/';
  
  
    constructor(private http: HttpClient) { }
  
    getEmployeeById(id: number): Observable<any> {
      return this.http.get(`${this.apiUrl}${id}`); 
    }
    updateEmployee(id: number, employee: Employee): Observable<any> {
        return this.http.put<any>(`${this.apiUrl}${id}`, employee);
      }
  }