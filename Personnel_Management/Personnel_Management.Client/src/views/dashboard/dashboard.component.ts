import { Component, AfterViewInit } from '@angular/core';
import { NavbarComponent } from "../../layout/navbar/navbar.component";
import { SidebarComponent } from "../../layout/sidebar/sidebar.component";
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [NavbarComponent, SidebarComponent, RouterOutlet],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent  {
  
}
