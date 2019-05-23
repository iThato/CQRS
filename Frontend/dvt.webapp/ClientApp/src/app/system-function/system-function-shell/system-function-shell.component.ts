import { Component, OnInit, TemplateRef, ViewChild, AfterViewInit } from '@angular/core';
import { SysfunctionPages } from './SysFunctionPpages.enum';
import { SysFunctionGroupItemComponent } from '../system-function-group/system-function-group-item/sysfunction-group-item.component';

@Component({
    selector: 'app-sysfunctionshell',
    templateUrl: './system-function-shell.component.html',
    styleUrls: ['./system-function-shell.component.scss']
})
export class SysFunctionShellComponent implements OnInit, AfterViewInit {

    @ViewChild('cardsTemplate')
    private cardsTemplate: TemplateRef<any>;

    @ViewChild('sysProfilesTemplate')
    private sysProfilesTemplate: TemplateRef<any>;

    @ViewChild('sysFunctionsTemplate')
    private sysFunctionsTemplate: TemplateRef<any>;

    @ViewChild('sysGroupsTemplate')
    private sysGroupsTemplate: TemplateRef<any>;

    public type: SysfunctionPages;

    constructor() {

    }

    public ngOnInit(): void {
    }

    ngAfterViewInit(): void {
    }

    public get template(): TemplateRef<any> {
        switch (this.type) {
            case SysfunctionPages.Profile:
                return this.sysProfilesTemplate;
            case SysfunctionPages.SystemFunction:
                return this.sysFunctionsTemplate;
            case SysfunctionPages.Group:
                return this.sysGroupsTemplate;
            default:
                return this.cardsTemplate;
        }
    }

    public viewProfiles(): void {
        this.type = SysfunctionPages.Profile;
    }

    public viewFunctions(): void {
        this.type = SysfunctionPages.SystemFunction;
    }

    public viewGroups(): void {
        this.type = SysfunctionPages.Group;
    }
}
