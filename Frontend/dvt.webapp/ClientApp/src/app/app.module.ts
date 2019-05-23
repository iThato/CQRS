import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA, MatTableModule, MatPaginatorModule } from '@angular/material';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { NotifierModule } from 'angular-notifier';
import { NgxSpinnerModule } from 'ngx-spinner';
import { AppComponent } from './app.component';
import { MainLayoutComponent } from './admin/main-layout/main-layout.component';
import { NavbarComponent } from './admin/navbar/navbar.component';
import { SidebarComponent } from './admin/sidebar/sidebar.component';
import { AuthGuard } from './auth.guard';
import { CounterComponent } from './counter/counter.component';
import { DialogComponent } from './dialog/dialog.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { MatConfirmDialogComponent } from './mat-confirm-dialog/mat-confirm-dialog.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { RegisterComponent } from './register/register.component';
import { SignalrComponent } from './signalr/signalr.component';
import { SysFunctionShellComponent } from './system-function';
import { SystemFunctionModule } from './system-function/system-function.module';
import { AdduserComponent, UserComponent, UserListComponent } from './user';
import { MaterialModule } from './_modules/material.module';
import { DialogService, ModalService, ServicesModule, SignalRService } from './_services';
import { LoginGuard } from './authguards/login.guard';
import { UserService } from './_api/api/user.service';
import { AuthenticationService } from './_api/api/authentication.service';
import { AppRoutingModule } from './app.routing';
import { UserManagementComponent } from './user-management/user-management.component';
import { UserAddComponent } from './user-management/user-add/user-add.component';
import { CourseService } from './_api/api/course.service';
import { AuthourizationInterceptor } from './jwt-interceptor/jwt-interceptor';





@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    MainLayoutComponent,
    NavbarComponent,
    SidebarComponent,
    SignalrComponent,
    UserComponent,
    AdduserComponent,
    UserListComponent,
    MatConfirmDialogComponent,
    DialogComponent,
    UserManagementComponent,
    UserAddComponent
    

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    NgxSpinnerModule,
    FormsModule,
    MatPaginatorModule,
    MatTableModule,
    RouterModule,
    MatDialogModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MaterialModule,
    SystemFunctionModule,
    NotifierModule.withConfig({
      position: {
 
        horizontal: {
       
          /**
           * Defines the horizontal position on the screen
           * @type {'left' | 'middle' | 'right'}
           */
          position: 'right',
       
          /**
           * Defines the horizontal distance to the screen edge (in px)
           * @type {number} 
           */
          distance: 12
       
        },
       
        vertical: {
       
          /**
           * Defines the vertical position on the screen
           * @type {'top' | 'bottom'}
           */
          position: 'top',
       
          /**
           * Defines the vertical distance to the screen edge (in px)
           * @type {number} 
           */
          distance: 12
       
          /**
           * Defines the vertical gap, existing between multiple notifications (in px)
           * @type {number} 
           */
        
       
        }
       
      }
    }),
    ServicesModule,
    AppRoutingModule
    ,

  ],
  providers: [SignalRService, CourseService, UserService, DialogService, AuthenticationService, UserListComponent, AdduserComponent,
    { provide: HTTP_INTERCEPTORS, useClass: AuthourizationInterceptor, multi: true},
    { provide: MatDialogRef, useValue: {} },
    { provide: MAT_DIALOG_DATA, useValue: [] }
    , ModalService],
  bootstrap: [AppComponent],
  entryComponents: [AdduserComponent,UserAddComponent, MatConfirmDialogComponent]
})
export class AppModule { }
