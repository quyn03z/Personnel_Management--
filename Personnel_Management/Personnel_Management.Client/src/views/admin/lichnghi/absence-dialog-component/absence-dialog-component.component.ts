import { Component, inject, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { LichNghiService } from '../lichnghi.service';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-absence-dialog',
  standalone:true,
  imports: [CommonModule, MatInputModule,MatDatepickerModule, MatNativeDateModule,FormsModule],
  templateUrl: './absence-dialog-component.component.html',
  styleUrls: ['./absence-dialog-component.component.scss']  // Optional: Add styles for the dialog
})
export class AbsenceDialogComponent {
  reason: string;

  http = inject(HttpClient)

  constructor(
    public dialogRef: MatDialogRef<AbsenceDialogComponent>,  // Reference to the dialog
    @Inject(MAT_DIALOG_DATA) public data: any,  // Data passed to the dialog (date and reason)
    private lichNghiService: LichNghiService  // Service to handle API requests
  ) {
    this.reason = data.reason;  // Initialize the reason field with the passed data
  }

  // Save the updated reason
  // onSave(): void {
  //   const date = this.data.date;
  //   console.log(date +''+ this.reason); 
  //   if (this.reason) {
  //     this.lichNghiService.updateLichNghi(date.getDate(), date.getMonth() + 1, date.getFullYear(), this.reason)
  //       .subscribe((res: any) => {
  //         if(res){
  //           this.dialogRef.close();  // Close the dialog after saving
  //         }  
  //       });
  //   }
  // }

  onSave(): void {
    const date = this.data.date;
    console.log(date +''+ this.reason); 
    if (this.reason) {
      this.http.put(`https://localhost:7182/api/LichNghi/UpdateLichNghiByExactDate?day=14&month=11&year=2024`, "a").subscribe((res:any) =>{
        if(res){
          this.dialogRef.close();  // Close the dialog after saving
        }
      })
      
    }
  }

  // Delete the absence record
  onDelete(): void {
    const date = this.data.date;
    this.lichNghiService.deleteLichNghi(date.getDate(), date.getMonth() + 1, date.getFullYear())
      .subscribe(() => {
        this.dialogRef.close();  // Close the dialog after deleting
      });
  }

  // Close the dialog without making any changes
  onClose(): void {
    this.dialogRef.close();
  }
}
