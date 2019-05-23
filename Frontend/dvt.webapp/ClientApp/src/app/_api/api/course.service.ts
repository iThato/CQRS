import { Injectable, Optional, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpResponse, HttpEvent } from '@angular/common/http';
import { Configuration } from '../configuration';
import { BASE_PATH, BASE_PATH_2 } from '../variables';
import { Observable } from 'rxjs/Observable';
import { CustomHttpUrlEncodingCodec } from '../encoder';
import { CourseResponse } from '../model/courseResponse';


@Injectable({
  providedIn: 'root'
})
export class CourseService {

  protected basePath = 'https://localhost:4200';
  protected basePath_2 = 'http://localhost:51976'; 
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH_2) basePath_2: string, @Optional() configuration: Configuration) {
      if (basePath_2) {
          this.basePath_2 = basePath_2;
      }
      if (configuration) {
          this.configuration = configuration;
          this.basePath_2 = basePath_2 || configuration.basePath_2 || this.basePath_2;
      }
  }

   /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
      const form = 'multipart/form-data';
      for (let consume of consumes) {
          if (form === consume) {
              return true;
          }
      }
      return false;
  }


  
    /**
     * 
     * 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiUserGetAllCoursesGet(observe?: 'body', reportProgress?: boolean): Observable<CourseResponse>;
    public apiUserGetAllCoursesGet(observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<CourseResponse>>;
    public apiUserGetAllCoursesGet(observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<CourseResponse>>;
    public apiUserGetAllCoursesGet(observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set("Accept", httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        let consumes: string[] = [
        ];

        return this.httpClient.get<CourseResponse>(`${this.basePath_2}/api/Course/GetAllCourse`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param id 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public apiUserGetCourseByIdGet(id?: number, observe?: 'body', reportProgress?: boolean): Observable<CourseResponse>;
    public apiUserGetCourseByIdGet(id?: number, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<CourseResponse>>;
    public apiUserGetCourseByIdGet(id?: number, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<CourseResponse>>;
    public apiUserGetCourseByIdGet(id?: number, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (id !== undefined) {
            queryParameters = queryParameters.set('Id', <any>id);
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'text/plain',
            'application/json',
            'text/json'
        ];
        let httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set("Accept", httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        let consumes: string[] = [
        ];

        return this.httpClient.get<CourseResponse>(`${this.basePath_2}/api/Course/GetCourseById`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }
  }
