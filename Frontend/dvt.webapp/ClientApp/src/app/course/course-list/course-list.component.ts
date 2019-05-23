import { Component, OnInit, Input, ViewChild, SimpleChanges } from '@angular/core';
import { NotifierService } from 'angular-notifier';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { NgxSpinnerService } from 'ngx-spinner';
import { DialogService } from '../../_services/dialog.service';
import { ModalService } from '../../_services/modal.service';
import { CourseService } from '../../_api/api/course.service';
import { Router } from '@angular/router';
import { AuthGuard } from '../../auth.guard';
// import { Router } from '@angular/router';

@Component({
  selector: 'app-course-list',
  templateUrl: './course-list.component.html',
  styleUrls: ['./course-list.component.css']
})
export class CourseListComponent implements OnInit {
 
  courses: any[];
  private notifier: NotifierService;
  coursesdatesource: MatTableDataSource<any>;
  displayColumns: string[] = ['Code', 'Name', 'Description', 'actions'];

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private authGuard: AuthGuard, private dialogService: DialogService, private spinner: NgxSpinnerService, private modalservice: ModalService, private courseservice: CourseService, notifier: NotifierService, private dialogservice: DialogService) { this.notifier = notifier; }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {

    this.courseservice.apiUserGetAllCoursesGet().subscribe(result => {
      this.courses = result.result;

      this.coursesdatesource = new MatTableDataSource(this.courses);
      this.coursesdatesource.sort = this.sort;
      this.coursesdatesource.paginator = this.paginator;
      console.log(this.coursesdatesource);
    }, error => {
      this.notifier.notify("error", "Oops! something went wrong");
    })

  }

}

