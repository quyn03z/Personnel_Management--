import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { LichNghi, LichNghiService } from './lichnghi.service';
import { AbsenceDialogComponent } from './absence-dialog-component/absence-dialog-component.component';

@Component({
  selector: 'app-lichnghi',
  standalone: true,
  imports: [MatInputModule, MatDatepickerModule, MatNativeDateModule],
  templateUrl: './lichnghi.component.html',
  styleUrls: ['./lichnghi.component.scss']
})
export class LichnghiComponent implements OnInit {
  selectedDate: Date | null = null;
  absenceReasons: { [date: string]: string } = {};  // Track reasons by date

  constructor(
    private lichNghiService: LichNghiService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadAbsences();
  }

  loadAbsences(): void {
    this.lichNghiService.getAllLichNghiOnMonth().subscribe(data => {
      data.forEach((item: LichNghi) => {
        this.absenceReasons[new Date(item.Ngay).toISOString().split('T')[0]] = item.Lydo;
      });
    });
  }

  onSelect(date: Date): void {
    this.selectedDate = date;
    const dateKey = date.toISOString().split('T')[0];
    const reason = this.absenceReasons[dateKey] || 'No absence reason';

    // Open the dialog with the selected date and reason
    const dialogRef = this.dialog.open(AbsenceDialogComponent, {
      data: { 
        date: date, 
        lichNghi: { lichNghiId: 0, Ngay: date, Lydo: reason }  // Initialize with a new LichNghi object
      }
    });

    // After the dialog closes, reload the absences to reflect any updates
    dialogRef.afterClosed().subscribe(() => {
      this.loadAbsences();
    });
  }

  // addAbsence(date: Date, reason: string): void {
  //   this.lichNghiService.addLichNghi({ ngay: date, lyDo: reason }).subscribe(() => {
  //     this.absenceReasons[date.toISOString().split('T')[0]] = reason;
  //   });
  // }

  updateAbsence(date: Date, newReason: string): void {
    const [day, month, year] = [date.getDate(), date.getMonth() + 1, date.getFullYear()];
    const updatedLichNghi: LichNghi = { lichNghiId: 0, Ngay: date, Lydo: newReason };

    this.lichNghiService.updateLichNghi(day, month, year, updatedLichNghi).subscribe(() => {
      this.absenceReasons[date.toISOString().split('T')[0]] = newReason;
    });
    console.log(`Updated reason: ${newReason}`);
  }

  deleteAbsence(date: Date): void {
    const [day, month, year] = [date.getDate(), date.getMonth() + 1, date.getFullYear()];
    this.lichNghiService.deleteLichNghi(day, month, year).subscribe(() => {
      delete this.absenceReasons[date.toISOString().split('T')[0]];
    });
  }
}
