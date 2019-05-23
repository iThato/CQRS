import { Routes } from '@angular/router';

import { DashboardComponent } from '../dashboard/dashboard.component';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { UserListComponent } from '../../user-management/user-list/user-list.component';
import { CourseListComponent } from '../../course/course-list/course-list.component';
import { AuthGuard } from '../../auth.guard';

export const MainLayoutRoutes: Routes = [

    { path: 'dashboard',      component: DashboardComponent },
    { path: 'user-profile',   component: UserProfileComponent },
    { path: 'user-list',   canActivate: [AuthGuard],  component: UserListComponent },
    { path: 'course-list', canActivate: [AuthGuard],    component: CourseListComponent }
      
];

