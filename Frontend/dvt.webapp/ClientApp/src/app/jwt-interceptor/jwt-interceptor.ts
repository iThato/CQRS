import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { TokenService } from "../_services/token.service";
import { Observable } from "rxjs";

@Injectable()

export class AuthourizationInterceptor implements HttpInterceptor {
    
    constructor (private tokenService: TokenService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if(this.tokenService.isLogged()){
            const authToken = this.tokenService.getAuthorizationToken();
            req = req.clone({
                setHeaders:
                { 'Authorization': 'Bearer ' + authToken }
            });
        }
        return next.handle(req);
    }
   
}