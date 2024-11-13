import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http'; // Required for HTTP requests
import { Router } from '@angular/router'; // Required for navigation
import { FormsModule } from '@angular/forms'; // Required for ngModel binding
import { CommonModule } from '@angular/common'; // Required for Angular common functionalities
import { RouterLink } from '@angular/router'; // Required for routerLink in the template
import { DeparmentListComponent } from '../deparment-list.component'; // Import DeparmentListComponent
@Component({
  selector: 'app-view-add-department',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule, 
    RouterLink,  // Import RouterLink for navigation in the template
  ],
  templateUrl: './add-department.component.html',
  styleUrls: ['./add-department.component.scss'],
})
export class AddDepartmentComponent {
  tenPhongBan: string = ''; // Department name
  moTa: string = ''; // Description

  constructor(private http: HttpClient, private router: Router) {}

  onSubmit() {
    const departmentData = {
      tenPhongBan: this.tenPhongBan,
      moTa: this.moTa,
    };

    // Sending the HTTP POST request to add a department
    this.http.post('https://localhost:7182/api/Department/add', departmentData).subscribe({
      next: () => {
        alert('Phòng ban đã được thêm thành công!');
        //location.reload(); // Tải lại trang sau khi thêm thành công
        this.router.navigate(['/admin/departmentList']); 
      },
      error: (error) => {
        console.error('Lỗi khi thêm phòng ban:', error);
        alert('Lỗi khi thêm phòng ban. Vui lòng thử lại sau.');
      }
    });
  }
}
