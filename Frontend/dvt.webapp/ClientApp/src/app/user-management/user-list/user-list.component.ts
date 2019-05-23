import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { MatTableDataSource, MatSort, MatPaginator, } from '@angular/material';
import { NgxSpinnerService } from 'ngx-spinner';
import { ModalService } from '../../_services/modal.service';
import { UserService } from '../../_api/api/user.service';
import { DialogService } from '../../_services/dialog.service';
import { UserAddComponent } from '../user-add/user-add.component';
import { AuthGuard } from '../../auth.guard';
import { AdduserComponent } from '../../user';
// import { Router } from '@angular/router';


@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users: any[];
  private notifier: NotifierService;
  usersdatesource: MatTableDataSource<any>;
  displayColumns: string[] = ['firstName', 'lastName', 'email', 'contactNumber', 'username', 'systemProfile', 'knownAs', 'actions'];

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private authGuard: AuthGuard, private dialogService: DialogService, private spinner: NgxSpinnerService, private modalservice: ModalService, private userservice: UserService, notifier: NotifierService, private dialogservice: DialogService) { this.notifier = notifier; }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {

    this.userservice.apiUserGetAllUsersGet().subscribe(result => {
      this.users = result.result;

      this.usersdatesource = new MatTableDataSource(this.users);
      this.usersdatesource.sort = this.sort;
      this.usersdatesource.paginator = this.paginator;
      console.log(this.usersdatesource);
    }, error => {
      this.notifier.notify("error", "Oops! something went wrong.");
    })

  }

  onCreateOrEdit() {
    this.modalservice.openDialog(AdduserComponent, {
      title: 'Add New User',
      buttonName: 'Add User',
      isEdit: 1
    }, '100%').subscribe(res => {
      this.getUsers();
    });

  }

  disableUser(row) {
    this.dialogService.openconfirmdialog("Are you sure you want to disable " + row.firstName + " " + row.lastName + "?")
      .afterClosed().subscribe(result => {
        if (result) {
          this.userservice.apiUserDisableUserPost(row.userAccountId).subscribe(result => {
            if (result.result) {
              this.notifier.notify("success", "User Successfully Disbled. Reloading Acconts...");
              this.getUsers();
            }

          }, error => {
            this.notifier.notify("error", "Could not diable " + row.firstName + " " + row.surname);
          });
        }
      });
  }
}

