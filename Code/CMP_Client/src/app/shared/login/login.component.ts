import { Component, OnInit } from '@angular/core';
import { NgForm,  NgModel } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { User } from 'src/Models/User';
import { AuthHttpService } from 'src/app/Services/HttpServices/AuthHttpService';
import { NgForOf } from '@angular/common';

export interface Role {
  id:number,
  role:string
}


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  AdminForm !: NgForm
  admin !: User
  selectedRole !: number
  roles : Array<Role> = [{id:0,role:'Admin'}, {id:0,role:'Doctor'},{id:0,role:'Nurse'},{id:0,role:'Wardboy'},{id:0,role:'receptionist'},{id:0,role: 'Housekeeping'}]

  constructor(private authService : AuthHttpService) {
    this.admin = new User();
   }

  ngOnInit(): void {
    this.admin = new User();
  }

  submit(loginform : NgForm):void {
    debugger
    console.log(loginform.value)
    this.admin.username = loginform.value.username;
    this.admin.password = loginform.value.password;
    this.admin.role = "admin";
    if(loginform.value.isPersistent != ""){
      this.admin.isPersistent = loginform.value.isPersistent
    }else {
      this.admin.isPersistent = false;
    }

    console.log(this.admin)
    this.authService.login(undefined, this.admin);


  }

}
