import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-confirm-otp',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './confirm-otp.component.html',
  styleUrl: './confirm-otp.component.scss'
})
export class ConfirmOtpComponent {
  confirmOtp = {
    email: '',
    otp: '',
    token: '',
  }
  errorMessage: string = '';

  constructor(private router: Router, private authService: AuthService) {
    const storedEmail = localStorage.getItem('otpEmail');
    const storedToken = localStorage.getItem('otpToken');
    
    if (storedEmail) {
      this.confirmOtp.email = storedEmail;
    } else {
      alert('No email found, please request OTP again.');
      this.router.navigate(['/send-otp']);
      return;
    }

    if (storedToken) {
      this.confirmOtp.token = storedToken; 
    } else {
      alert('No token found, please request OTP again.');
      this.router.navigate(['/send-otp']);
      return;
    }
  }

  confirmOTP() {
    console.log('Confirm OTP Payload:', JSON.stringify(this.confirmOtp));

    if (this.confirmOtp.email && this.confirmOtp.otp && this.confirmOtp.token) {
      this.authService.confirmOTP(this.confirmOtp).subscribe({
        next: (data: any) => {
          console.log('OTP verification success:', data);
          alert('OTP confirmed successfully!');

          const { passwordResetToken  } = data;
          if (passwordResetToken ) {
            console.log('Storing reset token in localStorage and navigating to change-password');
            localStorage.setItem('PasswordResetToken', passwordResetToken );
            this.router.navigate(['/change-password']);
          } else {
            console.error('Password reset token is missing in response data');
          }
        },
        error: (err) => {
          console.error('Error details from backend:', err);
          this.errorMessage = err.error?.message || 'Invalid OTP or email. Please try again.';
        }
      });
    } else {
      this.errorMessage = 'Please enter the OTP.';
    }
  }



}
