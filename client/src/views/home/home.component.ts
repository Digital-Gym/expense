import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ApiService } from '../../services/api/api.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    ButtonModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  constructor(private api: ApiService){}

  showInfo(){
    console.log(this.api.getStats());
  }
}
