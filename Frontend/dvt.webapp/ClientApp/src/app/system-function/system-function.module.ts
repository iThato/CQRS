import { NgModule, Optional, SkipSelf, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { SysFunctionShellComponent } from '.';
import { SystemFunctionService } from '../_api';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../_modules/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SysFunctionGroupItemComponent } from './system-function-group/system-function-group-item/sysfunction-group-item.component';
import { SystemFunctionDetailsService } from './system-function.service';
import { SysFunctionGroupListComponent } from './system-function-group/system-function-group-list/sysfunction-group-list.component';
import { SysFunctionGroupAddComponent } from './system-function-group/system-function-group-add/sysfunction-group-add.component';

@NgModule({
    imports: [FormsModule,
        CommonModule,
        MaterialModule, ReactiveFormsModule],
    exports: [SysFunctionShellComponent, SysFunctionGroupItemComponent, SysFunctionGroupListComponent, SysFunctionGroupAddComponent],
    declarations: [SysFunctionShellComponent, SysFunctionGroupItemComponent, SysFunctionGroupListComponent, SysFunctionGroupAddComponent],
    providers: [SystemFunctionService, SystemFunctionDetailsService]
})
export class SystemFunctionModule {
    constructor() {
    }
}
