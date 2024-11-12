import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { param } from 'jquery';

@Component({
  selector: 'app-edit-department',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './edit-department.component.html',
  styleUrls: ['./edit-department.component.scss']
})
export class EditDepartmentComponent implements OnInit {
  phongBanId: number| null = null;
  tenPhongBan: string = '';
  moTa: string = '';

  http = inject(HttpClient);
  route = inject(ActivatedRoute);
  router = inject(Router);

  ngOnInit(): void {
    // this.phongBanId = Number(this.route.snapshot.paramMap.get('phongBanId'));
    // this.getDepartmentById(this.phongBanId);
    this.route.params.subscribe(params =>{
      this.phongBanId = +params['id'];
      if(this.phongBanId){
        this.getDepartmentById(this.phongBanId);
      }else{
        console.log('loi');
      }
    })
  }

  getDepartmentById(id: number) {
    this.http.get(`https://localhost:7182/api/Department/${id}`).subscribe(
      (res: any) => {
        if (res) {
          this.tenPhongBan = res.tenPhongBan;
          this.moTa = res.moTa;
        } else {
          alert('Không tìm thấy phòng ban.');
        }
      },
      (error) => {
        console.error('Lỗi khi lấy dữ liệu phòng ban:', error);
        alert('Không thể lấy dữ liệu phòng ban');
      }
    );
  }
  onSubmit() {
    const departmentData = {
      phongBanId: this.phongBanId,
      tenPhongBan: this.tenPhongBan,
      moTa: this.moTa,
    };

    this.http.put(`https://localhost:7182/api/Department/${this.phongBanId}`, departmentData).subscribe({
      next: () => {
        alert('Cập nhật phòng ban thành công!');
        this.router.navigate(['/admin/departmentList']);
      },
      error: (error) => {
        console.error('Error updating department:', error);
        alert('Lỗi khi cập nhật phòng ban. Vui lòng thử lại.');
      }
    });
  }
}
