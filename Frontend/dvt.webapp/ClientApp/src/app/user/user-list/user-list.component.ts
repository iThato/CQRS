import { Component, OnInit, ViewChild, Input, SimpleChanges } from '@angular/core';
import { UserService } from '../../_api/api/user.service';
import { MatTableDataSource } from '@angular/material';
import { MatSort, MatPaginator } from '@angular/material';
import { NotifierService } from 'angular-notifier';
import { DialogService } from '../../_services/dialog.service';
import { ModalService } from '../../_services/modal.service';
import { AdduserComponent } from '../adduser/adduser.component';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  @Input() public users: any[];
  searchKey: string;
  private notifier: NotifierService;
  usersdatesource: MatTableDataSource<any>;

  displayColumns: string[] = ['firstName', 'lastName', 'email', 'contactNumber', 'systemProfile', 'knownAs', 'userStatus', 'actions'];

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private spinner: NgxSpinnerService, private modalservice: ModalService, private userservice: UserService, notifier: NotifierService, private dialogService: DialogService) { this.notifier = notifier; }

  ngOnInit() {
    this.displayUsersFromParentComponent();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['users']) {
      this.displayUsersFromParentComponent();
    }
  }

  displayUsersFromParentComponent() {
    this.usersdatesource = new MatTableDataSource(this.users);
    this.usersdatesource.sort = this.sort;
    this.usersdatesource.paginator = this.paginator;
  }

  onSearchClear() {
    this.searchKey = "";
    this.applyFilter();
  }


  applyFilter() {
    this.usersdatesource.filter = this.searchKey.trim().toLocaleLowerCase();
  }

  diableUser(row) {
    this.dialogService.openconfirmdialog("Are you sure you want to disable " + row.firstName + " " + row.lastName + "?")
      .afterClosed().subscribe(result => {
        if (result) {
          this.userservice.apiUserDisableUserPost(row.userAccountId).subscribe(result => {
            if (result.result) {
              this.notifier.notify("success", "User Successfully Disbled. Reloading Acconts...");
              location.reload();
            }

          }, error => {
            this.notifier.notify("error", "Could not delete " + row.firstName + " " + row.surname);
          });
        }
      });
  }
  onEdit(row) {
    this.modalservice.openDialog(AdduserComponent, {
      title: 'Edit User',
      buttonName: 'Save Changes',
      isEdit: 2,
      entity: row
    }, '1200px');

  }

}

