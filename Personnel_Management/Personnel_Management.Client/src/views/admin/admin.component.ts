import { RouterOutlet, Router } from '@angular/router';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChartData, ChartOptions } from 'chart.js'; 
import { Chart, registerables } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [RouterOutlet, CommonModule,BaseChartDirective ],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent {
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
  }

  getDepartmentData() {
    this.http.get<{ $values: { phongBanId: number; tenPhongBan: string; totalNhanVien: number }[] }>('https://localhost:7182/api/Department/TotalNhanVienInPhongBan')
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
}