import { Component } from '@angular/core';
import { ApiService } from '../../services/api/api.service';
import { randomColor, formatCurrency } from '../../misc/utils';

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

@Component({
  selector: 'app-history',
  standalone: true,
  imports: [],
  templateUrl: './history.component.html',
  styleUrl: './history.component.css'
})
export class HistoryComponent {
  expenses: Expense[] = [];
  pairs: ColorPair = {};
  formatCurrency = formatCurrency;

  constructor(private api: ApiService){
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
}
