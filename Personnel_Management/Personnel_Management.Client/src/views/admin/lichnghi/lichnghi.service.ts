import { Injectable } from '@angular/core';
import { HttpClient,HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
export interface LichNghi {
  lichNghiId: number;
  Ngay:Date;
  Lydo:string;
}

@Injectable({
  providedIn: 'root'
})
export class LichNghiService {
  private baseUrl = 'https://localhost:7182/api/LichNghi';  // Update with actual backend URL

  constructor(private http: HttpClient) {}

  getAllLichNghiOnMonth(): Observable<any> {
    return this.http.get(`${this.baseUrl}/GetAllLichNghiOnMonth`);
  }

  // addLichNghi(data: { ngay: Date, lyDo: string }): Observable<any> {
  //   return this.http.post(`${this.baseUrl}/AddLichNghi`, data);
  // }

  updateLichNghi(day: number, month: number, year: number, lichnghi: LichNghi): Observable<any> {
    const params = new HttpParams()
      .set('day', day.toString())
      .set('month', month.toString())
      .set('year', year.toString());

    return this.http.put<any>(`${this.baseUrl}/UpdateLichNghiByExactDate`, lichnghi, { params })
      .pipe(
        catchError(error => {
          console.error('Error updating LichNghi:', error);
          throw error;
        })
      );
  }

  deleteLichNghi(day: number, month: number, year: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/DeleteLichNghiByExactDate`, {
      params: { day: day.toString(), month: month.toString(), year: year.toString() }
    });
  }
}
