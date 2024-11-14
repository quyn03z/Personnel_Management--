import { RouterOutlet, Router, ActivatedRoute } from '@angular/router';
import { AfterViewInit,Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartData, ChartOptions } from 'chart.js'; 
import { Chart, registerables } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [RouterOutlet, CommonModule,BaseChartDirective ,FormsModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent {
  totalEmployee:number |null=null;
  totalDepartment:number|null=null;
  numberOfDepartments: number = 5;
  departmentList: any[] = [];
  isAdminDashboard: boolean = true;
  chartData!: ChartData<'pie'>; 
  chartOptions: any = {
    responsive: true,
  };

  constructor(private router: Router,private http: HttpClient) {
    this.router.events.subscribe(() => {
      this.isAdminDashboard = this.router.url === '/admin';
    });
  }

  ngOnInit(): void {
    Chart.register(...registerables);
    this.getDepartmentData();
    this.getTotalEmployees();
    this.getTotalDepartments();
    this.getAllDepartment();
  }

  getDepartmentData() {
    this.http.get<{ $values: { phongBanId: number; tenPhongBan: string; totalNhanVien: number }[] }>(`https://localhost:7182/api/Department/TopTotalNhanVienInPhongBan?count=${this.numberOfDepartments}`)
      .pipe(
        map(response => response.$values)
      )
      .subscribe({
        next: data => {
          const colors = data.map(() => `rgba(${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, 0.5)`);
          const borderColors = colors.map(color => color.replace('0.5', '1')); // Đổi opacity cho borderColor

          this.chartData = {
            labels: data.map(item => item.tenPhongBan),
            datasets: [{
              label: 'Số lượng nhân viên',
              data: data.map(item => item.totalNhanVien),
              backgroundColor: colors,
              borderColor: borderColors,
              borderWidth: 1
            }]
          };
        },
        error: error => {
          console.error('Lỗi khi lấy dữ liệu từ API:', error);
        }
      });
  }
  getTotalEmployees() {
    this.http.get<number>('https://localhost:7182/api/NhanViens/total-employees')
      .subscribe({
        next: totalEmployees => {
          this.totalEmployee = totalEmployees;
        },
        error: error => {
          console.error('Lỗi khi lấy tổng số nhân viên từ API:', error);
        }
      });
  }
  getTotalDepartments() {
    this.http.get<number>('https://localhost:7182/api/Department/total-Department')
      .subscribe({
        next: totalDepartments => {
          this.totalDepartment = totalDepartments;
        },
        error: error => {
          console.error('Lỗi khi lấy tổng số nhân viên từ API:', error);
        }
      });
  }
  getDepartments() {
    this.getDepartmentData();  // Gọi lại để lấy dữ liệu mới khi người dùng thay đổi số lượng phòng ban
  }
  getAllDepartment(): void {
    this.http.get("https://localhost:7182/api/Department/TotalNhanVienInPhongBan").subscribe((res: any) => {

      this.departmentList = res.$values;
      console.log(this.departmentList);

    });
  }
}