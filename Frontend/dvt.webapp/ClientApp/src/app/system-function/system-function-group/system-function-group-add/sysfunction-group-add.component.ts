import { OnInit, Component, Input } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NotifierService } from 'angular-notifier';
import { SystemFunctionService } from '../../../_api/api/systemFunction.service';

@Component({
    selector: 'app-sysgroupadd',
    templateUrl: './sysfunction-group-add.component.html',
    styleUrls: ['./sysfunction-group-add.component.scss'],
})
export class SysFunctionGroupAddComponent implements OnInit {
    public groupForm: FormGroup;
    private notifier: NotifierService;

    constructor(private _systemFunctionService: SystemFunctionService,
        _notifier: NotifierService) {
        this.notifier = _notifier;

        this.groupForm = new FormGroup({
            Name: new FormControl('', [Validators.required]),
            displayName: new FormControl('')
        });
    }
    public ngOnInit(): void {
    }

    public addGroup(): void {
        this._systemFunctionService.apiSystemFunctionAddSystemFunctionGroupPost(this.groupForm.value).subscribe(result => {
            this.notifier.notify('success', 'Group Successfully Added');
            location.reload();

        }, error => {
            this.notifier.notify('error', error.error[0].errorMessage);
        });

    }
}
