import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service'; // đảm bảo đường dẫn đúng
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-login-signup',
  standalone: true,
  imports: [FormsModule, CommonModule, HttpClientModule],
  templateUrl: './login-signup.component.html',
  styleUrls: ['./login-signup.component.scss']
})
export class LoginSignupComponent implements OnInit {

  loginObj = { 
    email: '',
    matKhau: ''
  };

  loginFailed: boolean = false;

  constructor(private router: Router, private authService: AuthService) {} 

  @Output() login = new EventEmitter<void>();

  ngOnInit(): void {
    // Bất kỳ thao tác khởi tạo nào khác
  }

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
