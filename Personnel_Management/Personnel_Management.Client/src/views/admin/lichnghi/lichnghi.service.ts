import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LichNghiService {
  private baseUrl = 'https://localhost:7182/api/LichNghi';  // Update with actual backend URL

  constructor(private http: HttpClient) {}

  getAllLichNghiOnMonth(): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetAllLichNghiOnMonth`);
  }

  addLichNghi(data: { ngay: Date, lyDo: string }): Observable<any> {
    return this.http.post(`${this.baseUrl}/AddLichNghi`, data);
  }

  updateLichNghi(day: number, month: number, year: number, newLyDo: string="HOang"): Observable<any> {
     const headers = { 'Content-Type': 'application/json' };
    return this.http.put<any>(`${this.baseUrl}/UpdateLichNghiByExactDate?day=${day}&month=${month}&year=${year}`, `hoang`
      , { headers }
    );
  }

  deleteLichNghi(day: number, month: number, year: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/DeleteLichNghiByExactDate`, {
      params: { day: day.toString(), month: month.toString(), year: year.toString() }
    });
  }
}
