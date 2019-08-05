import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<WeatherForecast[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }

  onClick() {
    try{
      this.http.get('https://localhost:44397/api/user/accounts/13').subscribe((data: any) => console.log(data));
    } catch (error) {
      throwError(error);
    }
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
