import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Notify } from 'notiflix';
import { Clinic } from 'src/Models/Clinic';
import { ClinicHttpService } from 'src/app/Services/HttpServices/ClinicHttpService';
import { StoreService } from 'src/app/Services/Store/StoreService';
import { ShiftTimeClass } from 'src/Models/ShiftTimeClass';
import { Dropdown } from 'src/Models/Dropdown';

@Component({
  selector: 'app-new-clinic',
  templateUrl: './new-clinic.component.html',
  styleUrls: ['./new-clinic.component.css']
})
export class NewClinicComponent implements OnInit {
  clinic :Clinic =  <Clinic>{};
  isUpdate : boolean = false;
  phone !: number
  departments :Array<Dropdown>= [{id:0, value:'Cardiology'},{id:1,value: 'Dermatology'},{id:2, value:'Neorology'},{id:3,value:'Psychology'}]
  constructor(private clinicService : ClinicHttpService, private route:ActivatedRoute, private store : StoreService, private router:Router) {
    if(this.route.snapshot.paramMap.get('mode') == "update") {
      this.isUpdate = true;
      this.clinic = this.store.clinic;
      this.phone = Number(this.clinic.telephone)
    }
   }

  ngOnInit(): void {
  }

  submit() {

    if(this.clinic.department == undefined || this.clinic.department == null) {
      Notify.failure("Error : Department is Required.")
      return;
    }
    // this.clinic.telephone = this.clinic.telephone+"";
    this.clinic.department = Number(this.clinic.department);
    if(this.isUpdate){
      this.clinicService.update(undefined, this.clinic)
      .subscribe({
        next:(r) => console.log("formdata"+r),
        error: err => console.log("Err",err),
        complete : () => {
          console.log("updated")
        }
      })
    }else{
      this.clinicService.create(undefined, this.clinic)
      .subscribe({
        next:(r) => console.log("formdata"+r),
        error: err => console.log("Err",err),
        complete : () => {
          console.log("created.")
        }
      })
    }
  }
}
