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

  onDateSelected(event: Date): void {
    this.selectedDate = event;
  }
}
