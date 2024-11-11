import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{

  ngOnInit(): void {
  }

  loginObj = { 
    email: '',
    matKhau: ''
  };


  loginFailed: boolean = false;

  constructor(private router: Router, private authService: AuthService) {} 

  @Output() login = new EventEmitter<void>();

  onLogin() { 
    this.authService.login(this.loginObj.email, this.loginObj.matKhau).subscribe(
      response => {
        // Nếu đăng nhập thành công, điều hướng đến trang Home
        this.login.emit();
        this.router.navigate(['/viewEmployeeList']);
      },
      error => {
        this.loginFailed = true; // Hiển thị thông báo đăng nhập thất bại
        console.error('Lỗi khi đăng nhập', error);
      }
    );
  }

  
}
