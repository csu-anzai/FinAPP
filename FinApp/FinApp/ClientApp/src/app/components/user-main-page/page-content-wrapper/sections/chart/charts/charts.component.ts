import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { User } from 'src/app/models/user';
import { Account } from 'src/app/models/account';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.css']
})
export class ChartsComponent implements OnInit {
  
  user: User = new User();
  accounts : Account[];
  chartLabels : string[];
 
  constructor(private _userService: UserService, private _authService: AuthService) { }
  
  ngOnInit() {
      this._userService.getUser(this._authService.DecodedToken.sub).subscribe(res => {
        this.accounts = res.accounts;
        console.log(this.accounts);
       // this.pieChartLabels= this.accounts[0].incomes.map(income=>income.incomeCategory.name);
        //this.pieChartData = this.accounts[0].incomes.map(income=>income.transaction.sum);
        console.log(this.pieChartLabels);
      });
    

  }
  
  
  title = 'app';
  public pieChartLabels: string[] = ["sport","food","restaurant"];
  public pieChartData: number[] = [25,46,30];
  public pieChartType: string = 'pie';
  public pieChartOptions: any = { 'backgroundColor': [
            "#d63e3e",
            "#61cc49",
            ]
          }
 
  // events on slice click
  public chartClicked(e:any):void {
    console.log(e);
  }
 
 // event on pie chart slice hover
  public chartHovered(e:any):void {
    console.log(e);
  }
  
}
