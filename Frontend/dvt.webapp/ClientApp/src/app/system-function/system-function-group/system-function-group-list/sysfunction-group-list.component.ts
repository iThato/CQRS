import { OnInit, Inject, Component, Input, AfterViewInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NotifierService } from 'angular-notifier';
import { SystemFunctionService } from '../../../_api/api/systemFunction.service';
import { SystemFunctionDetailsService } from '../../system-function.service';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-sysgrouplist',
    templateUrl: './sysfunction-group-list.component.html',
    styleUrls: ['./sysfunction-group-list.component.scss'],
})
export class SysFunctionGroupListComponent implements OnInit {

    public groupAdd = false;
    public noGroups: boolean;
    public groups: any;
    private notifier: NotifierService;

    constructor(private _systemFunctionDetailService: SystemFunctionService,
        _notifier: NotifierService) {
        this.notifier = _notifier;
    }
    public ngOnInit(): void {
        this._systemFunctionDetailService.apiSystemFunctionGetSystemFunctionGroupsGet().subscribe(res => {
            this.groups = res.result;
        }, error => {
            this.notifier.notify('error', error.error[0].errorMessage);
        }, () => {
                if (!this.groups) {
                    this.noGroups = true;
                }
            });

    }

    public addNewGroup(): void {
        this.groupAdd = true;
    }
}
