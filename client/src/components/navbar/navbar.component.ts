import { Component } from '@angular/core';
import { SidebarModule } from 'primeng/sidebar';
import { ButtonModule } from 'primeng/button';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    ButtonModule,
    SidebarModule,
    RouterLink
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})

export class NavbarComponent {
  sidebarVisible = false;

  hideBar(){
    this.sidebarVisible = false;
  }

  showBar(){
    this.sidebarVisible = true;
  }
}
