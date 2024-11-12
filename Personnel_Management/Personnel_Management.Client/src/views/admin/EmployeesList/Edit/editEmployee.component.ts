import { AfterViewInit, Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ActivatedRoute,Router } from '@angular/router';
import {EmployeeService,  Employee} from '../employee.service';

@Component({
  selector: 'app-view-edit-employee',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive,HttpClientModule,FormsModule,],
  templateUrl: './editEmployee.component.html',
  styleUrls: ['./editEmployee.component.scss']
})


export class EditEmployeesComponent implements  OnInit {
  
  roles = [
    { id: 1, name: 'Admin' },
    { id: 2, name: 'Manager' },
    { id: 3, name: 'Employee' },
  ];
  selectedRoleId: number | undefined;

  departments: { phongBanId: number; tenPhongBan: string }[] = [];
  selectedDepartment: number | undefined;

  employeeId: number | null=null;
  employee: Employee | null = null;

  http: HttpClient; 
 
  constructor(private route: ActivatedRoute, private employeeService: EmployeeService, private router: Router) {
    this.http = inject(HttpClient);
   }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.employeeId = +params['id'];
      if (this.employeeId) {
        this.getEmployeeDetails();
      } else {
        console.error("Employee ID not found in route parameters");
      }
    });

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

  getEmployeeDetails() {
    this.employeeService.getEmployeeById(this.employeeId!).subscribe({
      next: (data) => {
        this.employee = data;
        if (data.ngaySinh) {
          data.ngaySinh = this.formatDateForInput(data.ngaySinh);
        }
        this.selectedRoleId = data.roleId;
        this.selectedDepartment = data.phongBanId;
        console.log("Employee data after formatting:", this.employee);
      },
      error: (error) => console.error("Error fetching employee:", error)
    });
  }
  formatDateForInput(dateString: string): string {
    return dateString ? dateString.split('T')[0] : '';
  }
  
  submitUpdate() {
    if (!this.employee || !this.selectedRoleId || !this.selectedDepartment) {
      console.error("Dữ liệu chưa đầy đủ. Vui lòng kiểm tra lại.");
      return; 
    }
  
    // Log dữ liệu của employee trước khi gửi cập nhật
    console.log("Dữ liệu sau khi người dùng sửa:", this.employee);
  
    const updatedEmployee: Employee = {
      nhanVienId: this.employee.nhanVienId,
      hoTen: this.employee.hoTen,
      ngaySinh: this.employee.ngaySinh,
      diaChi: this.employee.diaChi,
      soDienThoai: this.employee.soDienThoai,
      email: this.employee.email,
      roleId: this.selectedRoleId,
      phongBanId: this.selectedDepartment,
      avatar: this.employee.avatar,
      matkhau:this.employee.matkhau
    };

    this.http.put(`https://localhost:7182/api/NhanViens/${this.employee.nhanVienId}`, updatedEmployee).subscribe({
      next: (response) => {
        alert("Sửa nhân viên thành công:");
        this.router.navigate(['/admin/employeesList']);
      },
      error: (error) => {
        console.log(this.employee);
        console.error("Lỗi khi thêm nhân viên:", error);

        alert('Lỗi khi thêm nhân viên. Vui lòng thử lại sau.');
      }
    });
  }
}
