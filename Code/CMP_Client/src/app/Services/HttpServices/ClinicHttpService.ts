import { AnyARecord } from "dns";
import { catchError, Observable, shareReplay, Subject, Subscriber, takeUntil, tap, throwError, timeout } from "rxjs";
import { Clinic } from "src/Models/Clinic";
import { HttpMethods } from "src/Models/Enums";
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { Filter } from "src/Models/filters";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Router } from "@angular/router";
import { Loading, Notify } from "notiflix";


@Injectable({
  providedIn: 'root'
})
export class ClinicHttpService {
  apiUrl = environment.url
  private ngUnsubscribe = new Subject<void>();
  constructor(private http: HttpClient, private router:Router) {}

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  getAll(url = this.apiUrl, filter? : Filter ) : Observable<Clinic[]> {
    Loading.circle("Fetching Clinics List")

    return this.http.get<Clinic[]>(url+"/clinic", this.httpOptions)
      .pipe(tap(_ => {
        Loading.remove()
        console.log('success')
        Notify.success("Clinic List fetched");
      }),
        catchError(this.handleError),
        takeUntil(this.ngUnsubscribe));
  }
  get(url = this.apiUrl, id: number, filter? : Filter ) : Observable<Clinic> {
    Loading.circle("Fetching Clinic Data")

    return this.http.get<Clinic>(url+"/clinic/details/"+id, this.httpOptions)
    .pipe( tap(_ => {
      Loading.remove()
    console.log('success')
    window.location.href='/home'
    Notify.success("Fetching Clinic Info");
    }),
        catchError(this.handleError),
        takeUntil(this.ngUnsubscribe));
  }
  create(url = this.apiUrl, clinic : Clinic ) : Observable<Clinic> {
    Loading.circle("Storing Clinic Data")
    return this.http.post<Clinic>(url+"/clinic/create", clinic, this.httpOptions)
    .pipe( tap(_ => {
      Loading.remove()
    console.log('success')

    Notify.success("Clinic Information Added Successfully.");
    this.router.navigate(['/home'])
    }),
        catchError(this.handleError),
        takeUntil(this.ngUnsubscribe));
  }

  delete(url = this.apiUrl, id : number ) : Observable<Clinic> {
    Loading.circle("Deleting Clinic Data")
    timeout(1000)
    return this.http.delete<Clinic>(url+"/clinic/delete/"+id, this.httpOptions)
    .pipe( tap(_ => {
      Loading.remove()
      console.log('success')
      Notify.success("Deleted Successfully.");
    }),
        catchError(this.handleError),
        takeUntil(this.ngUnsubscribe));
  }

  update(url = this.apiUrl, clinic : Clinic ) : Observable<Clinic> {
    Loading.circle("Updating Clinic Data")
    return this.http.put<Clinic>(url+"/clinic/update", clinic, this.httpOptions)
    .pipe( tap(_ => {
      Loading.remove()
      console.log('success')
      Notify.success("Data Updated Successfully.");
      this.router.navigate(['/home'])
    }),
        catchError(this.handleError),
        takeUntil(this.ngUnsubscribe));
  }

  handleError(error: any) {
    debugger
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      if(error.status == 400){
        errorMessage = 'Invalid Data : Please enter correct details'

      }else if(error.status == 401){
        errorMessage = `User Unauthorized, Please login to access`;
        this.router.navigate(['login'])
      }
      else if(error.status == 404)
      {
        errorMessage = 'Broken link'
      }else if(error.status >= 500 || error.status == 0)
      {
        errorMessage = 'Server down or malfunctioning.'
      }else {
        errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
      }
    }
    console.log(errorMessage);
    Loading.remove()
    Notify.failure("Error :"+errorMessage)
    return throwError(() => {
      return errorMessage;
    });
  }
}
