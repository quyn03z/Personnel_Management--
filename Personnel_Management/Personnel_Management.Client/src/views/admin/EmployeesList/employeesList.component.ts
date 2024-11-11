import { AfterViewInit, Component, inject, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-view-employee-list',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, DataTablesModule],
  templateUrl: './employeesList.component.html',
  styleUrls: ['./employeesList.component.scss']
})
export class EmployeesListComponent implements  OnInit {
  employeeList: any[] = [];
  http = inject(HttpClient);
  dtoptions: Config={};
  dttrigger: Subject<any> = new Subject<any>();

  ngOnInit(): void {
    this.getAllEmployee();
    this.dtoptions ={
      pagingType: 'full_numbers',
      lengthMenu:[5,8,15,20],
      pageLength: 10
    }
  }

  getAllEmployee(): void {
    this.http.get("https://localhost:7182/api/NhanViens").subscribe((res: any) => {
      
      this.employeeList = res.$values;
      console.log(this.employeeList);
      if (!this.dttrigger.closed) {
        this.dttrigger.next(null);
      }
    });
    
  }
  getRoleName(roleId: number): string {
    switch (roleId) {
        case 1:
            return 'Admin';
        case 2:
            return 'Manager';
        case 3:
            return 'Employee';
        default:
            return 'Unknown';
    }
}
deleteEmployee(id: number, isManager: boolean, newManagerId?: number): void {
  let url = `https://localhost:7182/api/NhanViens/${id}`;
  
  // Chỉ thêm newManagerId nếu isManager là true và newManagerId có giá trị
  if (isManager && newManagerId !== undefined) {
    url += `?newManagerId=${newManagerId}`;
  }
  console.log('ID:', id, 'isManager:', isManager, 'newManagerId:', newManagerId);
  this.http.delete(url).subscribe(
    (response) => {
      console.log(response); // In ra "Employee deleted."
      this.getAllEmployee();
      alert('Nhân viên đã được xóa.');
    },
    (error) => {
      console.error('Lỗi khi xóa nhân viên:', error);
      alert('Lỗi khi xóa nhân viên. Vui lòng thử lại.');
    }
  );
}confirmDeleteEmployee(id: number, roleId: number): void {
  if (roleId === 2) { // Kiểm tra nếu nhân viên là manager
    const newManagerId = prompt('Nhân viên này là manager. Vui lòng nhập ID của manager mới:');
    if (!newManagerId) {
      alert('Bạn phải nhập ID của manager mới để tiếp tục.');
      return;
    }

    // Gọi hàm xóa với ID của manager mới
    this.deleteEmployee(id, true, parseInt(newManagerId, 10));
  } else {
    // Gọi hàm xóa bình thường nếu không phải manager
    this.deleteEmployee(id, false);
  }
}

}