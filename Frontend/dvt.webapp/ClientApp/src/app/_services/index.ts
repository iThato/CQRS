import { NgModule } from '@angular/core';
import { WeatherService } from './weatherService';


@NgModule({
  providers: [
   
    WeatherService
  ]
})

export class ServicesModule { }

export * from './dialog.service';

export * from './modal.service';
export * from './signalr.service';

export * from './weatherService';
