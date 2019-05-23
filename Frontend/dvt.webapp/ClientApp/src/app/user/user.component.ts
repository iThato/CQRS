import { Component, OnInit, Input } from '@angular/core';
import { AdduserComponent } from './adduser/adduser.component';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material';
import { ModalService } from '../_services/modal.service';
import { UserService } from '../_api/api/user.service';


@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  users: any[];
  constructor(public dialogRef: MatDialogRef<AdduserComponent>,private modalservice: ModalService, public userService: UserService) { }

  ngOnInit() {
    this.getAllUsers();
  }

  onCreate() {
    this.modalservice.openDialog(AdduserComponent, {
      title: 'Add New User',
      buttonName: 'Add User',
      isEdit: 1
    }, '1200px').subscribe(res => {
      this.getAllUsers();
    });

    } 

  
  getAllUsers() {
    this.userService.apiUserGetAllUsersGet().subscribe(result => {
      this.users = result.result;
      console.log(this.users);
    });
  }


}


