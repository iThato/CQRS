import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MainLayoutRoutes } from '../main-layout/main-layout.routing';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { UserListComponent } from '../../user-management/user-list/user-list.component';
import { MatButtonModule, MatInputModule, MatRippleModule,MatTableModule,MatIconModule, MatFormFieldModule, MatTooltipModule, MatSelectModule, MatPaginatorModule } from '@angular/material';
import { CourseListComponent } from '../../course/course-list/course-list.component';




@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(MainLayoutRoutes),
    FormsModule,
    MatButtonModule,
    MatTableModule,
    MatPaginatorModule ,
    MatRippleModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule,
    MatTooltipModule,
   
  ],
  declarations: [
    DashboardComponent,
    UserProfileComponent,
    UserListComponent,
    CourseListComponent

  ]
})

export class MainLayoutModule { }
