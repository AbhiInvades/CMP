import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { interval } from 'rxjs';
import { AuthHttpService } from 'src/app/Services/HttpServices/AuthHttpService';
import { StoreService } from 'src/app/Services/Store/StoreService';
import { Location } from '@angular/common';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit, AfterViewInit {
  isLoggedIn : boolean = true;
  title !: string
  constructor(private authService : AuthHttpService, private store:StoreService, private router:Router, private _location:Location) {
    //call below function every second.
    this.isLoggedIn = this.authService.isLoggedIn();
    this.title = this.store.title;
   }

  ngOnInit(): void {
   // this.isLoggedIn = this.authService.isLoggedIn();
  }

  ngAfterViewInit(): void {
    //this.isLoggedIn = this.authService.isLoggedIn();
  }

  logOut():void {
    this.authService.logOut();
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  onclick(){
    console.log("i was clicked....")
    this.router.navigate(['/landing'])
  }

  back(){
    this._location.back();
  }

}
