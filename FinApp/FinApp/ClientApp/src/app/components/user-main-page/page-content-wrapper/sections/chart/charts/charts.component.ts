import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { Account } from 'src/app/models/account';
import { Chart } from 'chart.js';
import { map } from 'rxjs/operators'

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.css']
})
export class ChartsComponent implements OnInit {
  
  accounts : Account[];
  accountId : number = 0;
  constructor(private _userService: UserService, private _authService: AuthService) 
  {
    this._userService.getUser(this._authService.DecodedToken.sub).subscribe(res => {
    this.accounts = res.accounts;
    console.log(this.accounts);
    //this.pieChartLabels= this.accounts[this.accountId].incomes.map(income=>income.incomeCategory.name);
    //this.pieChartData = this.accounts[this.accountId].incomes.map(income=>income.transaction.sum);
    
  });
  }
  
  ngOnInit() {
      this._userService.getUser(this._authService.DecodedToken.sub).subscribe(res => {
      this.accounts = res.accounts;
        
      });
    

  }
  accountsChange(value){
    this.accountId = value;  
  }
  //title = 'app';
  public pieChartLabels: string[] = ["sport","food"];
  public pieChartData: number[] = [20,60];
  public pieChartType: string = 'pie';
  public pieChartOptions: any = { 'backgroundColor': [
            "#d63e3e",
            "#61cc49",
            ],
            responsive: true,
          }
 
  //events on slice click
  public chartClicked(e:any):void {
    console.log(e);
  }
 
 // event on pie chart slice hover
  public chartHovered(e:any):void {
    console.log(e);
  }
  
}
