import { Injectable } from '@angular/core';
import axios from 'axios';
import { URL } from '../../consts';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  userName: string = '';
  id: Number = 0;

  constructor() { 
    const storage = localStorage.getItem('expenseAuth');
    this.userName = storage ? JSON.parse(storage).name : '';
    this.id = storage ? JSON.parse(storage).id : '';

    this.test();
  }

  async login(name: string, password: string){
    try{
      const res = await axios.post(`${URL}/api/Auth/login`, {
        "username": name,
        "password": password
      });

      if(res && res.data.success){
        localStorage.setItem(
          'expenseAuth',
          JSON.stringify({ name, id: res.data.user.userId })
        );
        this.userName = name;
        return true;
      }
      return false;
    } catch(err){
      console.error(err);
      return false;
    }
  }

  async logout(){
    localStorage.removeItem("expenseAuth");
    this.userName = '';
    this.id = 0;
  }

  async test(){
    const temp = await axios.get(`${URL}/api/Expense/history`);
    console.log(temp.data);
  }

  getName(){
    return this.userName;
  }

  getId(){
    return this.id;
  }

  isAuthed(){
    if(this.userName){
      return true;
    }
    return false;
  }
}
