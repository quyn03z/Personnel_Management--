import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service'; 
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule] // Import ReactiveFormsModule and CommonModule here
})
export class SignupComponent {
  signUpObj = {
    hoTen: '',
    email: '',
    ngaySinh: '',
    diaChi: '',
    roleId: 3,
    matKhau: '',
    soDienThoai: ''
  };

  signUpForm: FormGroup;
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router, private fb: FormBuilder) {
    // Initialize form with validation
    this.signUpForm = this.fb.group({
      hoTen: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      ngaySinh: ['', Validators.required],
      diaChi: ['', Validators.required],
      roleId: [3],
      matKhau: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.pattern(/^(?=.*[!@#$%^&*(),.?":{}|<>]).+$/) 
        ]
      ],
      soDienThoai: [
        '',
        [
          Validators.required,
          Validators.pattern(/^((09|08|07|05|03)\d{8})$/) 
        ]
      ]
    });
  }

  get matKhau() {
    return this.signUpForm.get('matKhau');
  }

  get soDienThoai() {
    return this.signUpForm.get('soDienThoai');
  }

  onSignUp() {
    if (this.signUpForm.valid) {
      this.signUpObj = this.signUpForm.value;
      console.log(this.signUpObj);
      this.authService.signUpNhanVien(this.signUpObj).subscribe({
        next: (data: any) => {
          this.errorMessage = '';
          alert('Registration successful! Redirecting to login...');
          this.router.navigate(['/login']);
        },
        error: (err) => {
          if (err.status === 409) {
            this.errorMessage = 'An account with these details already exists. Please try a different email or username.';
          } else {
            this.errorMessage = 'Signup failed. Please check your details and try again.';
          }
        }
      });
    } else {
      this.errorMessage = 'Please fill out the form correctly.';
    }
  }
}
