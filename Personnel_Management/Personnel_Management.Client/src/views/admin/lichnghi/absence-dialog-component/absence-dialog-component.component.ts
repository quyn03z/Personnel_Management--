import { Component, inject, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { LichNghi, LichNghiService } from '../lichnghi.service';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-absence-dialog',
  standalone:true,
  imports: [CommonModule, MatInputModule,MatDatepickerModule, MatNativeDateModule,FormsModule],
  templateUrl: './absence-dialog-component.component.html',
  styleUrls: ['./absence-dialog-component.component.scss']  // Optional: Add styles for the dialog
})
export class AbsenceDialogComponent {
  selectedDate: Date;
  lichNghi$: Observable<LichNghi | null> = of(null);
  lichNghi:LichNghi = { lichNghiId: 0, ngay: new Date(), lyDo: 'Chưa có dữ liệu' };;
  http = inject(HttpClient)

  constructor(
    public dialogRef: MatDialogRef<AbsenceDialogComponent>,  // Reference to the dialog
    @Inject(MAT_DIALOG_DATA) public data: any,  // Data passed to the dialog (date and reason)
    private lichNghiService: LichNghiService  // Service to handle API requests
  ) {
    this.selectedDate = data.date;
    this.lichNghi$ = this.lichNghiService.getLichNghiByDay(
      this.selectedDate.getDate(),
      this.selectedDate.getMonth() + 1,
      this.selectedDate.getFullYear(),
    ); 
    this.lichNghi$.subscribe(_lichNghi => {
      this.lichNghi = _lichNghi || { lichNghiId: 0, ngay: this.selectedDate, lyDo: 'Chưa có dữ liệu' };
      console.log(_lichNghi);
    });
  }

  // Save the updated reason
  onSave(): void {
    const date = this.data.date;
    if (!date || !this.lichNghi) {
      console.error("Date or lichNghi is missing.");
      return;
    }
    console.log(`Saving LichNghi for date: ${date}, data:`, this.lichNghi);
    this.lichNghiService.updateLichNghi(date.getDate(), date.getMonth() + 1, date.getFullYear(), this.lichNghi)
      .subscribe(
        (res: any) => {
          if (res) {
            alert(`Save day: ${date.getDate()}/${date.getMonth()+1}/${date.getFullYear()} with LyDo:${this.lichNghi.lyDo}`);
            this.dialogRef.close();
          }
        },
        (error) => {
          console.error("Error updating LichNghi:", error);
        }
      );
  }
  onDelete(): void {
    const date = this.data.date;
    if (!date || !this.lichNghi) {
      console.error("Date or lichNghi is missing.");
      return;
    }
    console.log(`Delete LichNghi for date: ${date}, data:`, this.lichNghi);
    this.lichNghiService.deleteLichNghi(date.getDate(), date.getMonth() + 1, date.getFullYear())
      .subscribe(
        (res: any) => {
          if (res) {
            alert(`Deleted day: ${date.getDate()}/${date.getMonth()+1}/${date.getFullYear()}`);
            this.dialogRef.close();
          }
        },
        (error) => {
          console.error("Error delete LichNghi:", error);
        }
      );
  }

  // Close the dialog without making any changes
  onClose(): void {
    this.dialogRef.close();
  }
}
