import { HttpClient, HttpHeaders } from "@angular/common/http";
import { catchError, Observable, Observer, shareReplay, Subject, takeUntil, tap, throwError } from "rxjs";
import { AuthResult } from "src/Models/AuthResult";
import { compareAsc, isBefore, startOfToday, startOfYesterday } from "date-fns";
import { parseJSON } from "date-fns/esm";
import { User } from "src/Models/User";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { environment } from "src/environments/environment";
import { Loading, Notify } from "notiflix";
import { CookieService } from "ngx-cookie-service";

@Injectable({
  providedIn: 'root'
})
export class AuthHttpService {
  apiUrl = environment.url
  private ngUnsubscribe = new Subject<void>();
  date !: number
  constructor(private http: HttpClient, private router:Router, private cookieService:CookieService) {}

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  login(url=this.apiUrl, admin : User) : void {
    debugger
    Loading.circle("Loading")
    this.http.post<AuthResult>(url+"/login", admin, this.httpOptions)
    .pipe( tap(_ => {
      Loading.remove()
      console.log('success')
      window.location.href='/home'
      Notify.success("Login Successful");
    }),
      catchError(this.handleError),
      takeUntil(this.ngUnsubscribe))
    .subscribe(x =>
      {
        this.setSession(x);
        console.log("result",x);
        Loading.remove()

      }, e => console.log(e));
  }
  setSession(res: AuthResult) {
    localStorage.setItem('token', res.token);
    localStorage.setItem('expires_at', JSON.stringify(res.expiry));
  }

  logOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('expires_at');
    this.router.navigate([''])
  }

  getToken(){
    return localStorage.getItem('token')
  }

  isLoggedIn() {
    return localStorage.getItem("token") != null && isBefore(Date.parse(new Date().toLocaleString()), Date.parse(localStorage.getItem("expires_at")??startOfYesterday().toDateString()));
  }

  isLoggedOut() {
    return !this.isLoggedIn();
  }

  handleError(error: any) {
    console.log(error)
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      if(error.status == 400){
        errorMessage = `Invalid Credentials`;
      }else if(error.status == 404)
      {
        errorMessage = 'Broken link'
      }else if(error.status >= 500 || error.status == 0)
      {
        errorMessage = 'Server down or malfunctioning.'
      }else {
        errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
      }


    }
    Loading.remove()
    console.log(errorMessage)
    Notify.failure("Login Error : "+errorMessage)
    return throwError(() => {
      return errorMessage;
    });
  }
}
