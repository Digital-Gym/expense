import { Injectable } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { URL } from '../../consts';
import axios from 'axios';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  public date: Date[] = [this.minusDays(7), new Date(),];

  constructor(private auth: AuthService) { }

  private minusDays(days: number): Date{
    const now = new Date().getTime();
    const dif = 1000 * 60 * 60 * 24 * days;

    return new Date(now-dif);
  }

  formatDatePair(){
    return [this.date[0].toISOString(), this.date[1].toISOString()]
  }

  async getStats(){
    try{
      const d = this.formatDatePair();
      const res = await axios.get(`${URL}/api/Expense/stats?startDate=${d[0]}&finishDate=${d[1]}`);

      if(res && res.data){
        return res.data;
      }
    } catch(err){
      console.error(err);
    }
  }
        
  async getHistory(){
    try{
      const d = this.formatDatePair();
      const res = await axios.get(`${URL}/api/Expense/history?startDate=${d[0]}&finishDate=${d[1]}`);

      if(res && res.data){
        return res.data;
      }
    } catch(err){
      console.error(err);
    }
  }

  async getCategories(){
    try{
      const res = await axios.get(`${URL}/api/Category`);

      if(res && res.data){
        return res.data;
      }
    } catch(err){
      console.error(err);
    }
  }

  async createExpense(title: string, amount: number, categoryId: number){
    try{
      const res = await axios.post(`${URL}/api/Expense`, {
        title,
        amount,
        userId: this.auth.getId(),
        categoryId,
        date: new Date().toISOString()
      });

      if(res && res.data){
        return res.data;
      }
    } catch(err){
      console.error(err);
    }
  }
}
