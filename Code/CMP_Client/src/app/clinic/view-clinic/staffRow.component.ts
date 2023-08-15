import { AfterViewInit, Component, Input, OnInit, Output, EventEmitter } from "@angular/core";
import { Staff } from "src/Models/Staff";
import { StaffHttpService } from "src/app/Services/HttpServices/StaffHttpService";
import {ConsolePipe} from 'src/app/Pipes/console.pipe'
import { Gender, ShiftTime } from "src/Models/Enums";
import { differenceInYears, parse } from "date-fns";
import { StoreService } from "src/app/Services/Store/StoreService";
import { Router } from "@angular/router";
@Component({
  selector: 'app-staff-row',
  template: ``,

  styleUrls: [`./clinic.component.css`]
})


export class staffRowComponent implements OnInit, AfterViewInit {




  constructor(private staffService : StaffHttpService, private store:StoreService, private router:Router){


  }
  ngAfterViewInit(): void {

  }

  ngOnInit(): void {

  }







}
