import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-diemdanh',
  standalone: true,
  imports: [MatDatepickerModule, CommonModule, MatInputModule, MatNativeDateModule, MatDialogModule, ReactiveFormsModule],
  templateUrl: './diemdanh.component.html',
  styleUrls: ['./diemdanh.component.scss'],
})
export class DiemdanhComponent implements OnInit {

  @ViewChild('attendanceDialog') attendanceDialogTemplate!: TemplateRef<any>;
  diemDanhForm: FormGroup;

  attendanceRecords: any[] = [];

  nhanVienId = parseInt(localStorage.getItem('NhanVienId') || '0', 10);

  constructor(
    private router: Router,
    private authService: AuthService,
    private dialog: MatDialog,
    private fb: FormBuilder
  ) {
    this.diemDanhForm = this.fb.group({
      nhanVienId: this.nhanVienId,
      ngayDiemDanh: [this.getCurrentDate()],
      thoiGianVao: [this.getCurrentDate()],
      thoiGianRa: [this.getCurrentDate()],
      trangThai: [true],
      lyDoVangMat: ['']
    });
  }

  private getCurrentDate(): string {
    const now = new Date();
    return now.toISOString().split('T')[0];
  }


  currentAttendance = {
    date: new Date(),
    status: true
  };

  checkDiemDanh: boolean = false;

  ngOnInit(): void {
    const currentDate = new Date();
    const ngay = currentDate.getDate();
    const thang = currentDate.getMonth() + 1;
    const nam = currentDate.getFullYear();

    console.log(`Fetching attendance data for month: ${thang}, year: ${nam}`);
    this.authService.getDiemDanhNhanVien(thang, nam).subscribe(
      (response) => {
        console.log('Raw API response:', response);

        this.attendanceRecords = response?.$values || [];

        console.log('Processed attendance records:', this.attendanceRecords);
      },
      (error) => {
        if (error.status === 404) {
          console.warn('No attendance records found for the selected period.');
          this.attendanceRecords = [];
        } else {
          console.error('Error fetching attendance records:', error);
        }
      }
    );

    this.authService.checkNgayDiemDanh(ngay, thang, nam).subscribe(
      (response) => {
        console.log('Attendance check response:', response);
        this.checkDiemDanh = false;
      },
      (error) => {
        if (error.status === 400) {
          console.warn('No attendance record found for today. Showing check-in button.');
          this.checkDiemDanh = true;
       } else {
          console.error('Error checking attendance for today:', error);
          this.checkDiemDanh = false;
       }
      }
    );




  }

  selectedDate: Date | null = null;

  onDateSelected(event: Date): void {
    this.selectedDate = event;
    const thang = event.getMonth() + 1;
    const nam = event.getFullYear();

    this.authService.getDiemDanhNhanVien(thang, nam).subscribe(
      (response) => {
        console.log('Raw API response:', response);
        this.attendanceRecords = response?.$values || [];
        
      },
      (error) => {
        if (error.status === 404) {
          console.warn('No attendance records found for the selected period.');
          this.attendanceRecords = [];
        } else {
          console.error('Error fetching attendance records:', error);
        }
      }
    );

  }


  onDiemDanh(): void {
    this.dialog.open(this.attendanceDialogTemplate, {
      data: {
        date: this.currentAttendance.date,
        status: this.currentAttendance.status
      }
    });
  }

  onUpdateDiemDanh(): void {
    if (this.diemDanhForm.valid) {
      console.log(this.diemDanhForm.value)
      const payload = this.diemDanhForm.value;
      this.authService.diemDanhNhanVien(payload).subscribe(
        response => {
          console.log('Diem danh updated successfully:', response);
          this.dialog.closeAll();
          this.checkDiemDanh = false;
          this.loadTableDiemDanh();
        },
        error => {
          console.error('Error updating diem danh:', error);
        }
      );
    }
  }

  loadTableDiemDanh(){
    const currentDate = new Date();
    const thang = currentDate.getMonth() + 1;
    const nam = currentDate.getFullYear();
    this.authService.getDiemDanhNhanVien(thang, nam).subscribe(
      (response) => {
        console.log('Raw API response:', response);

        this.attendanceRecords = response?.$values || [];

        console.log('Processed attendance records:', this.attendanceRecords);
      },
      (error) => {
        if (error.status === 404) {
          console.warn('No attendance records found for the selected period.');
          this.attendanceRecords = [];
        } else {
          console.error('Error fetching attendance records:', error);
        }
      }
    );
  }





}
