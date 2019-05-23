import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
  styleUrls: ['./user-add.component.css']
})
export class UserAddComponent implements OnInit {
  public userForm: FormGroup;
  constructor() {

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

    });
  }

  ngOnInit() {
  }

}
