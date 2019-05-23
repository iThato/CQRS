import { InjectionToken } from '@angular/core';

export const BASE_PATH = new InjectionToken<string>('basePath');
export const BASE_PATH_2 = new InjectionToken<string>('basePath_2');
export const COLLECTION_FORMATS = {
    'csv': ',',
    'tsv': '   ',
    'ssv': ' ',
    'pipes': '|'
}
