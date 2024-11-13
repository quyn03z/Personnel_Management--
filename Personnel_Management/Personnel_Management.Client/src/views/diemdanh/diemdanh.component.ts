import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-diemdanh',
  standalone: true,
  imports: [MatDatepickerModule,CommonModule,MatInputModule,MatNativeDateModule],
  templateUrl: './diemdanh.component.html',
  styleUrls: ['./diemdanh.component.scss'],
})
export class DiemdanhComponent implements OnInit{
  
  attendanceRecords: any[] = [];

  constructor(private router: Router, private authService: AuthService) {}



  ngOnInit(): void {
    const currentDate = new Date();
    const thang = currentDate.getMonth() + 1;
    const nam = currentDate.getFullYear();
  
    console.log(`Fetching attendance data for month: ${thang}, year: ${nam}`);
  
    this.authService.getDiemDanhNhanVien(thang, nam).subscribe(
      (response) => {
        this.attendanceRecords = response;
        console.log('API response:', this.attendanceRecords);
      },
      (error) => {
        console.error('Error fetching attendance records:', error);
      }
    );
  }
  

  selectedDate: Date | null = null;


  onDateSelected(event: Date): void {
    this.selectedDate = event;
  }





}
