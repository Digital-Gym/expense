import { Component } from '@angular/core';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { AuthService } from '../../services/auth/auth.service';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    IconFieldModule, InputIconModule,
    InputTextModule, ButtonModule,
    FormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
  public username: string;
  public pass: string;
  
  constructor(private auth: AuthService, private router: Router) {
    this.username = '';
    this.pass = '';
  }

  async submit(){
    const canGo = await this.auth.login(this.username, this.pass);
    if(canGo){
      this.router.navigateByUrl('/')
    }
  }
}