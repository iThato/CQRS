import { Injectable } from '@angular/core';
import { SystemFunctionService } from '../_api';

@Injectable()
export class SystemFunctionDetailsService {

    constructor(private _systemFunctionService: SystemFunctionService) {

    }

    public getSystemFunctionGroupList(): any {
        this._systemFunctionService.apiSystemFunctionGetSystemFunctionGroupsGet();
    }
}