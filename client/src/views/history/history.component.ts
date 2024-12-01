import { Component } from '@angular/core';
import { ApiService } from '../../services/api/api.service';
import { randomColor, formatCurrency } from '../../misc/utils';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

interface Expense{
  amount: number;
  categoryLink: string;
  categoryName: string;
  date: string;
  expenseId: number;
  title: string;
}

interface ColorPair{
  [key: string]: string;
}

interface Category{
  categoryId: number;
  name: string;
  icon: string;
}

@Component({
  selector: 'app-history',
  standalone: true,
  imports: [
    DialogModule,
    ButtonModule,
    InputTextModule,
    InputNumberModule,
    DropdownModule,
    FormsModule,
    ToastModule
  ],
  providers: [MessageService],
  templateUrl: './history.component.html',
  styleUrl: './history.component.css'
})
export class HistoryComponent {
  expenses: Expense[] = [];
  pairs: ColorPair = {};
  formatCurrency = formatCurrency;
  visible: boolean = false;

  selectedExpenseId: number | undefined;
  title: string | undefined;
  amount: number | undefined;
  selectedCategory: Category | undefined;
  hcategories: Category[] | undefined;

  constructor(private api: ApiService, private messageService: MessageService){
    this.refresh();
  }

  async refresh(){
    const data = await this.api.getHistory();

    if(data){
      this.expenses = data;
    }
  }

  getLink(link: string){
    return `https://raw.githubusercontent.com/lucide-icons/lucide/main/icons/${link}.svg`;
  }

  getColor(categoryName: string){
    const col = this.pairs[categoryName];

    if(col){
      return col;
    }

    const newCol = randomColor();
    this.pairs[categoryName] = newCol;
    return newCol;
  }

  async showDialog(expense: Expense) {
    this.hcategories = await this.api.getCategories();
    this.visible = true;

    this.title = expense.title;
    this.amount = expense.amount;
    this.selectedExpenseId = expense.expenseId;
    
    // i hate js sometime really
    if(this.hcategories){
      const temp = JSON.parse(JSON.stringify(this.hcategories))
      this.selectedCategory = temp.filter((x:Category) => x.name == expense.categoryName)[0];
    }
  }

  async handleDelete(){
    if(this.selectedExpenseId){
      const status = await this.api.deleteExpense(this.selectedExpenseId);

      if(status){
        this.messageService.add({ severity: 'info', summary: 'Success', detail: 'Expense has been deleted' });
        this.refresh();
        this.visible = false;
        return;
      }

      this.messageService.add({ severity: 'danger', summary: 'Error', detail: 'Error occured while deleting' });
      return;
    }
    this.messageService.add({ severity: 'warning', summary: 'Warning', detail: 'Expense is not selected' });
  }

  async handleUpdate(){
    if(this.selectedExpenseId && this.title && this.amount && this.selectedCategory){
      const status = await this.api.updateExpense(this.selectedExpenseId, this.title, this.amount, this.selectedCategory.categoryId);

      if(status){
        this.refresh();
        this.visible = false;
        this.messageService.add({ severity: 'info', summary: 'Success', detail: 'Expense has been updated' });
        return;
      }
      this.messageService.add({ severity: 'danger', summary: 'Error', detail: 'Error occured while updating' });
    }
  }
}
