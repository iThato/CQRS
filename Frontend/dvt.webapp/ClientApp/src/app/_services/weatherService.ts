import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {WeatherForecast} from '../_models/weatherForecast';

@Injectable()
export class WeatherService {

  model: any;
  baseUrl: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getData() {
    return this.http.get<WeatherForecast[]>(this.baseUrl + 'api/SampleData/WeatherForecasts');
  }
}
