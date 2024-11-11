import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.scss'
})
export class ChangePasswordComponent implements OnInit {

  changePassWordObj = {
    email: '',
    passwordResetToken: '',
    newPassword: ''
  }

  errorMessage: string = '';

  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit(): void {
    const storedEmail = localStorage.getItem('otpEmail');
    if (storedEmail) {
      this.changePassWordObj.email = storedEmail;
    }
  
    const token = localStorage.getItem('PasswordResetToken');  // Retrieve token from localStorage
    if (token) {
      console.log('Token retrieved from localStorage in change-password component:', token);
      this.changePassWordObj.passwordResetToken = token;
    } else {
      this.errorMessage = 'Token not found. Please verify OTP again.';
      this.router.navigate(['/confirm-otp']); // Redirect if token is missing
    }
  }

  changePassWord() {
    if (this.changePassWordObj.newPassword) {
      this.authService.changeNewPassWord(this.changePassWordObj).subscribe({
        next: (response) => {
          alert('Password changed successfully!');
          localStorage.removeItem('otpEmail');
          localStorage.removeItem('otpToken'); 
          localStorage.removeItem('PasswordResetToken');  
          this.router.navigate(['/login']); 
        },
        error: (err) => {
          console.error('Error changing password:', err);
          this.errorMessage = err.error?.message || 'An error occurred. Please try again.';
        }
      });
    } else {
      this.errorMessage = 'Please enter a new password.';
    }
  }

}
