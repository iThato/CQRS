import { OnInit, Inject, Component, Input, AfterViewInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NotifierService } from 'angular-notifier';
import { SystemFunctionService } from '../../../_api/api/systemFunction.service';
import { group } from '@angular/animations';

@Component({
    selector: 'app-sysgroupitem',
    templateUrl: './sysfunction-group-item.component.html',
    styleUrls: ['./sysfunction-group-item.component.scss'],
})
export class SysFunctionGroupItemComponent implements OnInit {


    @Input() public group: any;
    private notifier: NotifierService;
    public get groupName(): string {
        if (this.group) {
            if (this.group.name && !this.group.displayName) {
                return this.group.name;
            } else {
                return this.group.displayName;
            }
        }
    }
    constructor(private _systemFunctionService: SystemFunctionService,
        _notifier: NotifierService) {
        this.notifier = _notifier;

    }
    public ngOnInit(): void {
    }

    public addGroup(): void {
    }
}
