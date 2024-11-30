import { Component, Output, EventEmitter } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { AuthService } from '../../services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  standalone: true,
  imports: [ButtonModule],
  templateUrl: './logout.component.html',
  styleUrl: './logout.component.css'
})

export class LogoutComponent {
  @Output() closing = new EventEmitter<boolean>();

  constructor(private auth: AuthService, private router: Router) {}

  handleClick(){
    console.log(this.auth.getName());
    this.auth.logout();
    this.router.navigateByUrl('/login');
    this.closing.emit(true);
  }
}
