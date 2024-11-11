import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { NavbarComponent } from '../layout/navbar/navbar.component';
import { SidebarComponent } from '../layout/sidebar/sidebar.component';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, SidebarComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {

  title = 'Personnel_Management.Client';
  standalonePages: string[] = ['/login-signup'];

  constructor(private router: Router, private authService: AuthService) {}

  isLoginPage(): boolean {
    return this.router.url === '/login' 
    || this.router.url === '/forgot-password'
    || this.router.url === '/confirm-otp'
    || this.router.url === '/change-password'
    || this.router.url === '/profile';
  }

  isStandalonePage(): boolean {
    return this.isLoginPage(); 
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']).then(() => {
      window.location.reload();
    });
  }

}
