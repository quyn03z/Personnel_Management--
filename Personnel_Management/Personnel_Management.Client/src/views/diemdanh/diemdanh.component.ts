import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
  selector: 'app-diemdanh',
  standalone: true,
  imports: [MatDatepickerModule,CommonModule,MatInputModule,MatNativeDateModule],
  templateUrl: './diemdanh.component.html',
  styleUrls: ['./diemdanh.component.scss'],
})
export class DiemdanhComponent {
  selectedDate: Date | null = null;

  // Sample attendance records
  attendanceRecords = [
    { date: new Date(2023, 10, 1), status: 'Present' },
    { date: new Date(2023, 10, 2), status: 'Absent' },
    { date: new Date(2023, 10, 3), status: 'Present' },
    { date: new Date(2023, 10, 4), status: 'Present' },
    { date: new Date(2023, 10, 5), status: 'Absent' },
    // Add more records as needed
  ];

  onDateSelected(event: Date): void {
    this.selectedDate = event;
  }
}
