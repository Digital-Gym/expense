import { Component } from '@angular/core';
import { SidebarModule } from 'primeng/sidebar';
import { ButtonModule } from 'primeng/button';
import { RouterLink } from '@angular/router';
import { LogoutComponent } from '../logout/logout.component';
import { CalendarModule } from 'primeng/calendar';
import { ApiService } from '../../services/api/api.service';
import { FormsModule } from '@angular/forms';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { DropdownModule } from 'primeng/dropdown';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { AvatarModule } from 'primeng/avatar';
import { AuthService } from '../../services/auth/auth.service';

interface Category{
  categoryId: number;
  name: string;
  icon: string;
}

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    ButtonModule,
    SidebarModule,
    RouterLink,
    LogoutComponent,
    CalendarModule,
    FormsModule,
    DialogModule,
    InputTextModule,
    InputNumberModule,
    DropdownModule,
    ToastModule,
    AvatarModule
  ],
  providers: [MessageService],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})

export class NavbarComponent {
  sidebarVisible = false;
  visible: boolean = false;
  categories: Category[] | undefined;
  selectedCategory: Category | undefined;
  title: string | undefined;
  amount: number | undefined;

  constructor(public api: ApiService, private messageService: MessageService, private auth: AuthService){}

  async ngOnInit() {
    this.categories = await this.api.getCategories();
  }

  showDialog() {
    this.visible = true;
  }

  async handleCreate(){
    this.visible = false;
    if(this.title && this.amount && this.selectedCategory){
      const data = await this.api.createExpense(this.title, this.amount, this.selectedCategory.categoryId);
      if(data){
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Expense has been added' });
        return;
      } 
      this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Smth went wrong' });
    }
  }

  hideBar(){
    this.sidebarVisible = false;
  }

  showBar(){
    this.sidebarVisible = true;
  }

  getLetter(){
    return this.auth.getName()[0];
  }

  getName(){
    return this.auth.getName();
  }
}
