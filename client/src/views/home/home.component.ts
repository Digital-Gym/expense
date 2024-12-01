import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ApiService } from '../../services/api/api.service';
import { ChartModule } from 'primeng/chart';
import { randomColor, formatCurrency } from '../../misc/utils';

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
  formatCurrency = formatCurrency;

  constructor(private api: ApiService){}

  async ngOnInit() {
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const stats = await this.api.getStats(); 

    if(stats){
      const colors: Set<string> = new Set();
      
      while(stats.labels.length != colors.size){
        colors.add(randomColor());
      }

      this.pairList = this.getAggregated(stats.labels, [...colors]);

      if(stats.dataset.length > 0){
        this.total = stats.dataset.reduce((a:number, b:number)=>a + b);
      } else{
        this.total = 0;
      }

      this.data = {
        labels: stats.labels.length > 0 ? stats.labels : ['No values'],
        datasets: [
            {
              data: stats.dataset.length > 0 ? stats.dataset : [1],
              backgroundColor: stats.dataset.length > 0 ?[...colors] : 'gray'
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

  getAggregated(labels: string[], colors: string[]){
    const result = labels.map((x, i) => {
      return {
        "label": x,
        "color": colors[i]
      }    
    });

    return result;
  }

  refresh(){
    this.ngOnInit();
  }
}
