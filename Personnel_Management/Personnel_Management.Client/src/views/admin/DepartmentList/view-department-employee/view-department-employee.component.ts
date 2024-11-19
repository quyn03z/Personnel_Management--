import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Subscription, filter } from 'rxjs';

interface NhanVien {
  nhanVienId: number;
  hoTen: string;
  ngaySinh: string;
  diaChi: string;
  soDienThoai: string;
  email: string;
  roleId: number;
  phongBanId: number;
  avatar: string | null;
  isManager: boolean;
}

@Component({
  selector: 'app-view-department-employee',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './view-department-employee.component.html',
  styleUrls: ['./view-department-employee.component.scss']
})
export class ViewDepartmentEmployeeComponent implements OnInit, OnDestroy {
  employees: NhanVien[] = [];
  departmentId: number = 0;
  phongBan: any;
  haveData: boolean = false;
  private routeSubscription: Subscription | undefined;
  private navigationSubscription: Subscription | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    // Lắng nghe sự thay đổi URL khi điều hướng giữa các phòng ban
    this.navigationSubscription = this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        this.loadData();
      });

    // Tải dữ liệu khi component khởi tạo lần đầu
    this.loadData();
  }

  loadData(): void {
    // Lấy `departmentId` từ URL
    this.departmentId = +this.route.snapshot.params['id'];
    this.getPhongBan();
    this.fetchEmployees();
  }

  fetchEmployees(): void {
    const apiUrl = `https://localhost:7182/api/Department/${this.departmentId}/employees`;
    this.http.get<any>(apiUrl).subscribe(
      (response) => {
        this.employees = response.$values || [];
        this.haveData = this.employees.length > 0;
      },
      (error) => {
        console.error('Error fetching employees:', error);
        this.haveData = false;
      }
    );
  }
  getPhongBan() {
    if (this.departmentId) {
      this.http.get(`https://localhost:7182/api/Department/${this.departmentId}`).subscribe((res: any) => {
        this.phongBan = res;
      })
    }

  }
  ngOnDestroy(): void {
    // Hủy đăng ký khi component bị hủy
    this.routeSubscription?.unsubscribe();
    this.navigationSubscription?.unsubscribe();
  }
}
