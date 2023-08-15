import { AnyARecord } from "dns";
import { catchError, Observable, Subject, Subscriber, takeUntil, tap, throwError } from "rxjs";
import { Staff } from "src/Models/Staff";
import { HttpMethods } from "src/Models/Enums";
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { Filter } from "src/Models/filters";
import { Injectable } from "@angular/core";
import {saveAs} from "file-saver";
import { map } from 'rxjs/operators';
import { environment } from "src/environments/environment";
import { Router } from "@angular/router";
import { Loading, Notify } from "notiflix";


@Injectable({
  providedIn: 'root'
})
export class StaffHttpService {
  apiUrl = environment.url
  photo !: any
  private ngUnsubscribe = new Subject<void>();
  constructor(private http: HttpClient,private router:Router) {}
  /*========================================
    CRUD Methods for consuming RESTful API
  =========================================*/
  // Http Options
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  httpOptionsMultiPart = {
    headers: new HttpHeaders({
      'Content-Type': 'multipart/form-data',
    })

  };

  getAll(url = this.apiUrl,id : number, filter? : Filter ) : Observable<Staff[]> {
    Loading.circle("Fetching Data")

    return this.http.get<Staff[]>(url+"/staff/"+id, this.httpOptions)

    .pipe( tap(_ => {
      Loading.remove()
      console.log('success')
      Notify.success("Staff details fetched successfully.");
    }),
        catchError(this.handleError),
        takeUntil(this.ngUnsubscribe));
  }
  get(url = this.apiUrl, id: number, filter? : Filter ) : Observable<Staff> {
    Loading.circle("Fetching Data")

    return this.http.get<Staff>(url+"/staff/details/"+id, this.httpOptions)
    .pipe( tap(_ => {
      Loading.remove()
      console.log('success')
      Notify.success("Staff Details fetched successfully.");
    }),
        catchError(this.handleError),
        takeUntil(this.ngUnsubscribe));
  }


  create(url = this.apiUrl, staff : Staff ) : Observable<Staff> {
    Loading.circle("Storing Data")

    return this.http.post<Staff>(url+"/staff/create", staff)
      .pipe( tap(_ => {
        Loading.remove()
        console.log('success')
        this.router.navigate(['/clinic'])
        Notify.success("Staff created successfully.");
      }),
        catchError(this.handleError),
        takeUntil(this.ngUnsubscribe));
  }

  delete(url = this.apiUrl, id : number ) : Observable<Staff> {
    Loading.circle("Deleting Staff from Database")

    return this.http.delete<Staff>(url+"/staff/delete/"+id)
      .pipe( tap(_ => {
        Loading.remove()
        console.log('success')
        Notify.success("Staff deleted");
      }),
        catchError(this.handleError),
        takeUntil(this.ngUnsubscribe));
  }

  update(url = this.apiUrl, staff : Staff ) : Observable<Staff> {
    Loading.circle("Making Changes")

    return this.http.put<Staff>(url+"/staff/update", staff, this.httpOptions)
      .pipe( tap(_ => {
        Loading.remove()
        console.log('success')
        this.router.navigate(['/clinic'])
        Notify.success("Staff Details Updated");
      }),
        catchError(this.handleError),
        takeUntil(this.ngUnsubscribe));
  }

  updateShift(url = this.apiUrl, staff : Staff ) : Observable<Staff> {
    Loading.circle("Making Changes")

    return this.http.put<Staff>(url+"/staff/shift", staff, this.httpOptions)
      .pipe( tap(_ => {
        Loading.remove()
        console.log('success')
        Notify.success("Shift Updated");
      }),
        catchError(this.handleError),
        takeUntil(this.ngUnsubscribe));
  }

  getPhoto(url = this.apiUrl, name: string) : Observable<Blob> {
    return this.http.get(url+"/images/"+name,{responseType:'blob'})
    .pipe( tap(_ => {
      console.log("Fetching images.")
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
