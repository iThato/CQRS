import { Component, Inject } from '@angular/core';
import { WeatherForecast } from '../_models/weatherForecast';
import { WeatherService } from '../_services/weatherService';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];

  constructor(weatherService: WeatherService) {
    weatherService.getData().subscribe(result => {
      this.forecasts = result;
      console.log(result);
    }, error => console.error(error));
  }
}


