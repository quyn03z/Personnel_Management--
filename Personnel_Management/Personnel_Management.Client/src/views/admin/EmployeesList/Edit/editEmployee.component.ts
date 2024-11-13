import { AfterViewInit, Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService, Employee } from '../employee.service';
import { SalaryService, Salary } from '../salary.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-view-edit-employee',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, HttpClientModule, FormsModule,],
  templateUrl: './editEmployee.component.html',
  styleUrls: ['./editEmployee.component.scss']
})


export class EditEmployeesComponent implements OnInit {

  

  roles = [
    { id: 1, name: 'Admin' },
    { id: 2, name: 'Manager' },
    { id: 3, name: 'Employee' },
  ];
  selectedRoleId: number | undefined;

  departments: { phongBanId: number; tenPhongBan: string }[] = [];
  selectedDepartment: number | undefined;
  currentDate: string="";
  employeeId: number | null = null;
  employee: Employee | null = null;
  luong: Salary | null = null;
  http: HttpClient;

  constructor(private route: ActivatedRoute, private employeeService: EmployeeService, private salaryService: SalaryService, private router: Router) {
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

    const today = new Date();
    this.currentDate = today.toISOString().split('T')[0];
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
    forkJoin({
      employee: this.employeeService.getEmployeeById(this.employeeId!),
      salary: this.salaryService.getLuongByNhanVienId(this.employeeId!)
    }).subscribe({
      next: (result) => {
        this.employee = result.employee;
        this.luong = result.salary;

        // Định dạng dữ liệu ngày nếu có
        if (this.employee?.ngaySinh) {
          this.employee.ngaySinh = this.formatDateForInput(this.employee.ngaySinh);
        }
        if (this.luong?.ngayCapNhat) {
          this.luong.ngayCapNhat = this.formatDateForInput(this.luong.ngayCapNhat);
        }

        // Thiết lập các thuộc tính khác
        this.selectedRoleId = this.employee?.roleId;
        this.selectedDepartment = this.employee?.phongBanId;

        console.log("Employee data after formatting:", this.employee);
        console.log("Luong data after formatting:", this.luong);
      },
      error: (error) => console.error("Error fetching data:", error)
    });
  }

  formatDateForInput(dateString: string): string {
    return dateString ? dateString.split('T')[0] : '';
  }

  submitProfileUpdate() {
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
      matkhau: this.employee.matkhau
    };

    this.http.put(`https://localhost:7182/api/NhanViens/${this.employee.nhanVienId}`, updatedEmployee).subscribe({
      next: (response) => {
        const userConfirmed = confirm("Sửa nhân viên thành công! Bạn có muốn quay về trang chính không?");
        if (userConfirmed) {
          this.router.navigate(['/admin/employeesList']);
        }
      },
      error: (error) => {
        console.log(this.employee);
        console.error("Lỗi khi thêm nhân viên:", error);

        alert('Lỗi khi thêm nhân viên. Vui lòng thử lại sau.');
      }
    });
  }
  submitLuongUpdate() {
    if (!this.luong) {
      this.luong = {
        nhanVienId: this.employee ? this.employee.nhanVienId : 0, 
        ngayCapNhat: new Date().toISOString().split('T')[0], 
        luongCoBan: '',
        phuCap: '' 
      };
    }
  
    // Kiểm tra dữ liệu lương đã đầy đủ chưa
    if (!this.luong) {
      alert("Dữ liệu chưa đầy đủ. Vui lòng kiểm tra lại.");
      return;
    }
  
    // Log dữ liệu của lương trước khi gửi cập nhật
    console.log("Dữ liệu sau khi người dùng sửa:", this.luong);

    const updatedLuong: Salary = {
      nhanVienId: this.luong.nhanVienId,
      ngayCapNhat: new Date().toISOString().split('T')[0],
      luongCoBan: this.luong.luongCoBan,
      phuCap: this.luong.phuCap
    };

    this.http.put(`https://localhost:7182/api/Luong/${this.luong.nhanVienId}`, updatedLuong).subscribe({
      next: (response) => {
        const userConfirmed = confirm("Sửa lương thành công! Bạn có muốn quay về trang chính không?");
        if (userConfirmed) {
          this.router.navigate(['/admin/employeesList']);
        }
      },
      error: (error) => {
        console.log(this.employee);
        console.error("Lỗi khi sửa lương:", error);

        alert('Lỗi khi sửa lương. Vui lòng thử lại sau.');
      }
    });
  }
}
