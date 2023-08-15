import { HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AuthHttpService } from "src/app/Services/HttpServices/AuthHttpService";


@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(private authService: AuthHttpService) { }
    intercept(req: HttpRequest<any>, next: HttpHandler) {
        const authToken = this.authService.getToken();
        req = req.clone({
            setHeaders: {
                Authorization: "Bearer " + authToken
            }
        });
        return next.handle(req);
    }
}
