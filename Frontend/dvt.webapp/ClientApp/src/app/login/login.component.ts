import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../_api/api/authentication.service';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TokenService } from '../_services/token.service';
import { NotifierService } from 'angular-notifier';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup;
  errormessage: string;
  private notifier: NotifierService;

  constructor(private authenticationService: AuthenticationService, notifier: NotifierService, private spinner: NgxSpinnerService, private router: Router, private tokenService: TokenService) {
    this.notifier = notifier;
    this.loginForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    });

  }

  ngOnInit() {
    this.tokenService.logout();
  }

  // register(){
  //   this.router.navigateByUrl('/register');
  // }

  login() {
    this.spinner.show();
    this.authenticationService.tokenPost(this.loginForm.value).subscribe(result => {
      if (result.token) {
        this.tokenService.setToken(result.token);
        this.router.navigateByUrl('/dashboard');
      }


    }, error => {
      switch (error.status) {
        case 401: {
          this.errormessage = ("You have provided wrong username or password.");
          break;
        }
        default: {
          this.errormessage = ("Oops! Something went wrong.");
          break;
        }
      }
      this.spinner.hide();
      
      this.notifier.notify("error", this.errormessage);
    })

  }
}
