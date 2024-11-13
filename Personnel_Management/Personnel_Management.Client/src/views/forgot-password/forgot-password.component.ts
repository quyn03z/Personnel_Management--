import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.scss'
})
export class ForgotPasswordComponent {
  emailSendOtp = {
    email: '',
  }
  errorMessage: string = '';

  emailSendOtpForm: FormGroup;

  constructor(private router: Router, private authService: AuthService, private fb: FormBuilder) {
    this.emailSendOtpForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }


  onSendOTP() {
    if (this.emailSendOtpForm.valid) {
        this.emailSendOtp = this.emailSendOtpForm.value;
        console.log(this.emailSendOtp);
        localStorage.setItem('otpEmail', this.emailSendOtp.email);
        this.authService.sendMailOTP(this.emailSendOtp).subscribe({
            next: (data: any) => {
                this.errorMessage = '';
                if (data && data.token) { // Use `data.token` to match the API response
                    localStorage.setItem('otpToken', data.token);
                }
                alert('Send OTP successful! Redirecting to Confirm OTP.');
                this.router.navigate(['/confirm-otp']);
            },
            error: (err) => {
                if (err.status == 404) {
                    this.errorMessage = 'Email does not exist.';
                } else {
                    this.errorMessage = 'An error occurred. Please try again.';
                }
            }
        });
    }
}


  


}
