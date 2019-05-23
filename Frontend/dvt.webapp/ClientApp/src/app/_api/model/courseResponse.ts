export interface CourseResponse {
    Id?: number;
    Code?: string;
    Name?: string;
    Description?: string;
    result?: Array<CourseResponse>;
}