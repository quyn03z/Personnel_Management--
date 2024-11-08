import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { HttpClientModule } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl = "https://localhost:44357/api";
  
  private headerCustom = {};

  constructor(private http: HttpClient ) { 
    this.headerCustom = { headers: {"Authorization": "Bearer " + localStorage.getItem("token")} }
  }

  login(data: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + "/Auth/Login", data);
  }


}
