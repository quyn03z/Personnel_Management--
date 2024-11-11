import { AfterViewInit, Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';

interface Role {
  id: number;
  name: string;
}

@Component({
  selector: 'app-view-edit-employee',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive,HttpClientModule,FormsModule,],
  templateUrl: './editEmployee.component.html',
  styleUrls: ['./editEmployee.component.scss']
})

export class EditEmployeesComponent implements  OnInit {
  hoTen: string = '';          
  ngaySinh: Date | null = null;  
  diaChi: string = '';        
  soDienThoai: string = '';   
  email: string = '';      
  matkhau: string = ''; 

  departments: { phongBanId: number; tenPhongBan: string }[] = [];
  selectedDepartment: number | undefined;

  roles: Role[] = [
    { id: 3, name: 'Employee' },
    { id: 2, name: 'Manager' },
    { id: 1, name: 'Admin' }
  ];
  selectedRole: Role | undefined;
  
  http: HttpClient; 


  constructor(private router: Router) {
    this.http = inject(HttpClient); // Inject HttpClient vào constructor
  }

  ngOnInit(): void {
    this.getDepartments();
  }

  getDepartments(): void {
    this.http.get<{ $values: { phongBanId: number; tenPhongBan: string }[] }>("https://localhost:7182/api/Department").subscribe({
      next: (response) => {
        this.departments = response.$values;
      },
      error: (error) => {
        console.error("Lỗi khi lấy danh sách phòng ban:", error);
        alert('Lỗi khi lấy danh sách phòng ban. Vui lòng thử lại sau.');
      }
    });
  }
  onSubmit() {

    const formData = {
      hoTen: this.hoTen,
      ngaySinh: this.ngaySinh,
      diaChi: this.diaChi, 
      soDienThoai: this.soDienThoai,
      email: this.email, 
      matkhau: this.matkhau,
      roleId: this.selectedRole?.id, 
      phongBanId: this.selectedDepartment,
    };
    console.log(formData);
    this.http.post("https://localhost:7182/api/NhanViens", formData).subscribe({
      next: (response) => {
        alert("Thêm nhân viên thành công:");
        this.router.navigate(['/admin/employeesList']);
      },
      error: (error) => {
        console.error("Lỗi khi thêm nhân viên:", error);

        alert('Lỗi khi thêm nhân viên. Vui lòng thử lại sau.');
      }
    });
  }
}
