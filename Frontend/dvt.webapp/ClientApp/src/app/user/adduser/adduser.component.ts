import { Component, OnInit, Inject, ChangeDetectorRef } from '@angular/core';
import { MatDialogRef, MatDialog, MAT_DIALOG_DATA } from '@angular/material';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../_api/api/user.service';
import { NotifierService } from 'angular-notifier';
import { NgxSpinnerService } from 'ngx-spinner';




@Component({
  selector: 'app-adduser',
  templateUrl: './adduser.component.html',
  styleUrls: ['./adduser.component.css']
})
export class AdduserComponent implements OnInit {

  usertypeslist: any[];
  private notifier: NotifierService;
  public userForm: FormGroup;
  isEdit: number;
  public users: any[];
  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private spinner: NgxSpinnerService, private userService: UserService,
    public dialogRef: MatDialogRef<AdduserComponent>, notifier: NotifierService) {
    this.isEdit = this.data.isEdit;
    this.notifier = notifier;

    this.userForm = new FormGroup({

      userAccountId: new FormControl(null),
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      contactNumber: new FormControl('', [Validators.required]),
      systemProfileId: new FormControl('', Validators.required),
      knownAs: new FormControl('', Validators.required),
      acceptedTerms: new FormControl(false),
      systemProfile: new FormControl('')

    })
    if (this.data.entity != null) {
      this.userForm.patchValue(this.data.entity);
    }

  }
  systemprofilelist = [
    { id: 1, value: 'Profile 1' },
    { id: 2, value: 'Profile 2' },
    { id: 3, value: 'Profile 3' },
  ]


  ngOnInit() {

  }

  onReset() {
    this.userForm.reset();
  }

  onClose() {
    this.userForm.reset();
    this.dialogRef.close();
  }

  userMethodRouter() {
    switch (this.isEdit) {
      case 1: {
        this.saveUser();
        break;
      }
      case 2: {
        this.editUser();
        break;
      }
      default: {
        //statements; 
        break;
      }
    }
  }

  editUser() {
    this.userService.apiUserUpdateUserPut(this.userForm.value).subscribe(result => {
      this.notifier.notify("success", "User Profle Successfully Updated");
      this.dialogRef.close();
      this.spinner.show();

      // I want to refresh the User List here

      this.spinner.hide();

    }, error => {
      this.notifier.notify("error", error.error[0].errorMessage);
      this.dialogRef.close();
    })
  }


  saveUser() {
    this.userService.apiUserAddUserPost(this.userForm.value).subscribe(result => {
      this.notifier.notify("success", "User Profile Successfully Added");
      this.dialogRef.close();
      this.spinner.show();

      // I want to refresh the User List here


      this.spinner.hide();

    }, error => {
      this.notifier.notify("error", error.error[0].errorMessage);
      this.dialogRef.close();
    });
  }


}
