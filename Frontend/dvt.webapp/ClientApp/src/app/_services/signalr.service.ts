import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Subject } from 'rxjs';

const WAIT_UNTIL_ASPNETCORE_IS_READY_DELAY_IN_MS = 2000;

@Injectable()
export class SignalRService {


  connectionEstablished = new Subject<Boolean>();
  private hubConnection: HubConnection;

  constructor() {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  sendChatMessage(username,message) {
    this.hubConnection.invoke('NewMessage', username,message).then(dd=>{
        console.log('Message sent');
    },ee=>{
        console.log(ee);
    });
  }

  private createConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('/hub')
      .build();
  }

  private startConnection() {
    setTimeout(() => {
      this.hubConnection.start().then(() => {
        console.log('Hub connection started');
        this.connectionEstablished.next(true);
      });
    }, WAIT_UNTIL_ASPNETCORE_IS_READY_DELAY_IN_MS);
  }

  private registerOnServerEvents(): void {
    this.hubConnection.on('messageReceived', (username: string, message: string) => {
        var today = new Date();
        console.log(today.toString()+ ' ' + username + ': ' + message);
    });
    this.hubConnection.on('NewUserLogin', (username: any) => {
        console.log(username.firstName + ': Has logged in now');
    });
  }
}
