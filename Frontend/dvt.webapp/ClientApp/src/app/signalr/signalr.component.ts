import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../_services/signalr.service';

@Component({
  selector: 'app-signalr',
  templateUrl: './signalr.component.html',
  styleUrls: ['./signalr.component.css']
})
export class SignalrComponent implements OnInit {

  model: any = {
    username: '',
    message:''
  };
  constructor(private signalrService: SignalRService) { }

  ngOnInit() {
  } 

  send() {
    this.signalrService.sendChatMessage(this.model.username,this.model.message);
  }
}
