import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ApiService } from '../../services/api/api.service';
import { ChartModule } from 'primeng/chart';

interface CategoryLabel{
  label: string;
  color: string;
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    ButtonModule,
    ChartModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  data: any;
  options: any;
  total: number | undefined;
  pairList: CategoryLabel[] | undefined;

  constructor(private api: ApiService){}

  async ngOnInit() {
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const stats = await this.api.getStats(); 

    if(stats){
      const colors: Set<string> = new Set();
      
      while(stats.labels.length != colors.size){
        colors.add(this.randomColor());
      }

      this.pairList = this.getAggregated(stats.labels, [...colors]);
      this.total = stats.dataset.reduce((a:number, b:number)=>a + b);

      this.data = {
        labels: stats.labels,
        datasets: [
            {
              data: stats.dataset,
              backgroundColor: [...colors]
            }
        ]
      };
    }

    this.options = {
      cutout: '60%',
        plugins: {
          legend: {
            labels: {
              color: textColor
            }
          }
        }
    };
  }

  randomColor(){
    const r = Math.floor(Math.random() * 255);
    const g = Math.floor(Math.random() * 255);
    const b = Math.floor(Math.random() * 255);

    return `rgb(${r},${g},${b})`;
  }

  public formatCurrency(value: number | undefined){
    if(value){
      return value.toLocaleString('en-US').replace(/,/g, ' ');
    }
    return 0;
  }

  getAggregated(labels: string[], colors: string[]){
    const result = labels.map((x, i) => {
      return {
        "label": x,
        "color": colors[i]
      }    
    });

    return result;
  }
}
