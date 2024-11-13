import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


  export interface Salary {
    nhanVienId: number;
    luongCoBan:string;
    phuCap:string;
    ngayCapNhat:string;
    
  }
  @Injectable({
    providedIn: 'root'
  })
  export class SalaryService {

    private apiUrl = 'https://localhost:7182/api/Luong/';
  
  
    constructor(private http: HttpClient) { }
    getLuongByNhanVienId(id: number): Observable<any> {
        return this.http.get(`${this.apiUrl}${id}`); 
      }
    updateLuong(id: number, salary: Salary): Observable<any> {
          return this.http.put<any>(`${this.apiUrl}${id}`, salary);
        }

  }