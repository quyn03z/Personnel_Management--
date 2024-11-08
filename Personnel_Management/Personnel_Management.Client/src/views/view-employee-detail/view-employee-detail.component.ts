import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataTablesModule } from 'angular-datatables';
import { Config } from 'datatables.net';
import { Subject } from 'rxjs';
@Component({
  selector: 'app-view-employee-detail',
  standalone: true,
  imports: [DataTablesModule],
  templateUrl: './view-employee-detail.component.html',
  styleUrl: './view-employee-detail.component.scss'
})
export class ViewEmployeeDetailComponent implements OnInit {
  employee!: any;
  employeeList: any[] =[];
  nhanVienId: any;
  http = inject(HttpClient);
  activatedRoute = inject(ActivatedRoute);
  dtoptions: Config={};
  dttrigger: Subject<any> = new Subject<any>();

  ngOnInit(): void {
    this.getEmployeeById();
    // this.getAllEmployee();

    this.dtoptions ={
      pagingType: 'simple_numbers',
      pageLength: 3,
      lengthChange: false
    }
  }
  // getAllEmployee(): void {
  //   this.http.get("https://localhost:7182/api/NhanViens/GetAllManagerFunction").subscribe((res: any) => {
      
  //     this.employeeList = res.$values;
  //     console.log(this.employeeList);
  //     this.dttrigger.next(null);
  //   });
  // }
  getEmployeeById(){
    this.nhanVienId = this.activatedRoute.snapshot.paramMap.get('nhanVienId');
    if(this.nhanVienId){
      this.http.get('https://localhost:7182/api/NhanViens/GetByIdManagerFunction?id='+this.nhanVienId).subscribe((res: any) =>{
        if (res){
          this.employee = res;
        }
      })
    }
  }
}
