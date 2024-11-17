import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Observable } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive,FormsModule,CommonModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.scss'
})
export class SidebarComponent implements OnInit{

  constructor(private router: Router, private authService: AuthService) {
    this.roles$ = this.authService.roles$;
    this.authService.loadRolesFromToken();
  }

  roles$: Observable<string[]>;

  ngOnInit(): void {
    this.roles$.subscribe((roles) => {
      console.log('Current user roles:', roles);
    });
  }

  hasRole(requiredRoles: string[]): boolean {
    return this.authService.hasRole(requiredRoles);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']).then(() => {
      window.location.reload();
    });
  }

  
  
}
