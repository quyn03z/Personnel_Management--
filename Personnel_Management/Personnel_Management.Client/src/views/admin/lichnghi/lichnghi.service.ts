import { Injectable } from '@angular/core';
import { HttpClient,HttpParams } from '@angular/common/http';
import { Observable,of } from 'rxjs';
import { catchError } from 'rxjs/operators';

export interface LichNghi {
  lichNghiId: number;
  ngay:Date;
  lyDo:string;
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

  getLichNghiByDay(day: number, month: number, year: number): Observable<LichNghi | null> {
    const params = new HttpParams()
      .set('day', day.toString())
      .set('month', month.toString())
      .set('year', year.toString());

    return this.http.get<LichNghi>(`${this.baseUrl}/GetLichNghiByExactedDay`, { params })
      .pipe(
        catchError(error => {
          console.error('Error getting LichNghi:', error);
          return of(null); 
        })
      );
  }
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
    const params = new HttpParams()
      .set('day', day.toString())
      .set('month', month.toString())
      .set('year', year.toString());

    return this.http.delete<any>(`${this.baseUrl}/DeleteLichNghiByExactDate`, { params })
      .pipe(
        catchError(error => {
          console.error('Error Delete LichNghi:', error);
          throw error;
        })
      );
  }
}
