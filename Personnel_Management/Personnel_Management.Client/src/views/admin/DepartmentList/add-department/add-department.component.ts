import { Component, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { DeparmentListComponent } from '../deparment-list.component';

@Component({
  selector: 'app-view-add-department',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './add-department.component.html',
  styleUrls: ['./add-department.component.scss'],
})
export class AddDepartmentComponent {
  tenPhongBan: string = '';
  moTa: string = '';
  http = inject(HttpClient);
  router = inject(Router);

  constructor(private departmentList: DeparmentListComponent) {}

  onSubmit() {
    const departmentData = {
      tenPhongBan: this.tenPhongBan,
      moTa: this.moTa,
    };

    this.http.post('https://localhost:7182/api/Department/add', departmentData).subscribe({
      next: () => {
        alert('Phòng ban đã được thêm thành công!');

        // Phát sự kiện làm mới danh sách phòng ban
        this.departmentList.refreshListSubject.next();

        // Điều hướng về danh sách phòng ban
        // this.router.navigate(['/admin/departmentList']);
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          this.router.navigate([
            `/admin/departmentList`
          ]);
        });
      },
      error: (error) => {
        alert('Lỗi khi thêm phòng ban. Vui lòng thử lại sau.');
      }
    });
  }
}
