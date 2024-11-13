import { trigger } from '@angular/animations';
import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';
declare var google: any;
@Component({
  selector: 'app-attendance-report',
  standalone: true,
  imports: [DataTablesModule],
  providers: [DatePipe], // Thêm DatePipe vào providers
  templateUrl: './attendance-report.component.html',
  styleUrl: './attendance-report.component.scss'
})
export class AttendanceReportComponent implements OnInit {

  nhanVienId!: any;
  employee!: any;
  luong: any;
  listDiemDanh: any[] = [];
  soNgayDiLam: any;
  ngayCongChuan: any;
  currentMonth!: string;
  http = inject(HttpClient);
  datePipe = inject(DatePipe);
  activatedRoute = inject(ActivatedRoute);
  router = inject(Router);
  dtOptions: Config = {};
  dtTrigger: Subject<any> = new Subject<any>();


  ngOnInit(): void {
    const today = new Date();
    this.currentMonth = this.datePipe.transform(today, 'MM/yyyy') || '';

    this.getEmployeeById();
    this.getLuong();
    this.getListDiemDanh();
    this.dtOptions = {
      pagingType: 'simple_numbers',
      pageLength: 3,
      lengthChange: false
    }


    // Chỉ tải Google Charts một lần và đặt callback
    google.charts.load('current', { packages: ['corechart'] });
    google.charts.setOnLoadCallback(() => {
      this.drawChart();
    });
  }

  drawChart() {
    // Kiểm tra nếu soNgayDiLam hoặc ngayCongChuan không có giá trị
    if (typeof this.soNgayDiLam === 'undefined' || typeof this.ngayCongChuan === 'undefined') {
      return;
    }

    // Tạo bảng dữ liệu
    const data = google.visualization.arrayToDataTable([
      ['Số buổi', 'Số buổi trên tháng'],
      ['Diem danh', this.soNgayDiLam],
      ['Chua diem danh', (this.ngayCongChuan - this.soNgayDiLam)]
    ]);

    // Thiết lập tùy chọn biểu đồ
    const options = {
      pieHole: 0.35,
      pieSliceTextStyle: {
        color: 'black',
      },
      width: 180,
      height: 150,
      chartArea: {
        left: 30,
        top: 0,
        right: 0,
        width: '100%',
        height: '100%'
      },
      fontSize: 13,
      colors: ['#1CC88A', '#E74A3B'],
      legend: { position: 'none' }
    };

    // Khởi tạo và vẽ biểu đồ
    const chart = new google.visualization.PieChart(document.getElementById('chart_div'));
    chart.draw(data, options);
  }

  getEmployeeById() {
    this.nhanVienId = this.activatedRoute.snapshot.paramMap.get('nhanVienId');
    if (this.nhanVienId) {
      this.http.get('https://localhost:7182/api/NhanViens/GetByIdManagerFunction?id=' + this.nhanVienId).subscribe((res: any) => {
        if (res) {
          this.employee = res;
        }
      })
    }
  }

  getListDiemDanh() {
    if (this.nhanVienId) {
      this.http.get('https://localhost:7182/api/DiemDanh/GetAllDiemDanhById?nhanVienId=' + this.nhanVienId).subscribe((res: any) => {
        if (res) {
          this.listDiemDanh = res.danhSachDiemDanh.$values.map((dd: any) => ({
            ...dd,
            ngayDiemDanh: this.datePipe.transform(dd.ngayDiemDanh, 'yyyy-MM-dd')
          }));
          this.dtTrigger.next(null);
        }
      })
    }
  }

  getLuong() {
    if (this.nhanVienId) {
      this.http.get(`https://localhost:7182/api/Luong/GetSalaryEmployee?nhanVienId=${this.nhanVienId}`).subscribe((res: any) => {
        this.luong = res;
        this.soNgayDiLam = res.soNgayDiLam;
        this.ngayCongChuan = res.ngayCongChuan;

        // Gọi vẽ biểu đồ khi có dữ liệu
        this.drawChart();
      });
    }
  }

  btnBack(){
    this.router.navigate(['/viewEmployeeDetail', this.nhanVienId]);
  }
}
