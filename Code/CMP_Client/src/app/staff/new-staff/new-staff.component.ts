import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { debug } from 'console';
import { addYears, differenceInYears, format, parse, parseISO } from 'date-fns';
import { Clinic } from 'src/Models/Clinic';
import { Gender, ShiftTime } from 'src/Models/Enums';
import { GenderClass } from 'src/Models/Gender';
import { ShiftTimeClass } from 'src/Models/ShiftTimeClass';
import { Staff } from 'src/Models/Staff';
import { StaffHttpService } from 'src/app/Services/HttpServices/StaffHttpService';
import { StoreService } from 'src/app/Services/Store/StoreService';
import { Dropdown } from 'src/Models/Dropdown';


@Component({
  selector: 'app-new-staff',
  templateUrl: './new-staff.component.html',
  styleUrls: ['./new-staff.component.css'],
})
export class NewStaffComponent implements OnInit {
  filename !: string
  form: any;
  shift !: ShiftTime
  shiftSelected !: number
  shifts : Array<ShiftTimeClass> = [{id:0, shift:'MORNING'}, {id:1, shift:'EVENING'}, {id:2, shift:'NIGHT'}]
  genderSelected !: number
  genders : Array<GenderClass> = [{id:0,gender:'MALE'},{id:1, gender:'FEMALE'}, {id:2, gender:'OTHER'}];
  photo !: File
  base64textString !: string
  clinicId !: number
  staff : Staff = <Staff>{};
  isUpdate : boolean = false;
  clinicSelected !: number;
  clinics !: Clinic[]
  clinic !: Clinic
  date !: Date
  clinicSelectedString !: string
  phone !: number
  roles : Array<Dropdown> = [{id:0,value:'Admin'},{id:1,value: 'Nurse'},{id:2,value:'Doctor'},{id:3,value:'Receptionist'}];
  validDob !:string
  notChangeImage = false
  notChangePassword = false

  constructor(private staffService : StaffHttpService, private store : StoreService, private route: ActivatedRoute, private router:Router) {
    console.log(addYears(new Date(), -18))
    this.validDob = format(addYears(new Date(), -18), 'yyyy-MM-dd')
    console.log(this.validDob)
    if(store.clinic) {
      this.clinic = store.clinic;
      this.clinics = this.store.clinics;
      console.log(this.clinic)
      this.clinicSelected = this.clinics.findIndex(x => x.clinicId == this.clinic.clinicId);
      this.clinicSelectedString = this.clinic.clinicId+":"+this.clinic.name;
      this.staff.clinicID = this.clinic.clinicId;
      console.log("clinicSelected",this.clinic)
      this.date = new Date(this.staff.dob);
    }
    if(this.route.snapshot.paramMap.get('mode') == 'update'){
      this.isUpdate = true;
      this.staff = this.store.staff;
      console.log(this.staff.dob)
      if(this.staff.dob){
      this.staff.dobS = this.getDobString(this.staff.dob)
      console.log(this.staff.dobS)
      this.staff.phone=Number(this.staff.phone)
      }

      this.notChangeImage = true;
      this.notChangePassword = true;

    }
  }

  ngOnInit(): void {

  }

  submit() {
    //let formData = new FormData();
    //debugger
    // Object.keys(this.form.controls).forEach(formControlName => {
    //   formData.append(formControlName,  JSON.stringify(this.form.get(formControlName).value)); })
    //   console.log(formData);
    //   formData.append('Name','asdfasdfa')
    //   formData.append('photo', this.photo);
    //   console.log(formData, formData.getAll, formData.get('photo'))

    this.staff.fileName = this.filename;
    this.staff.shiftTime = parseInt(this.staff.shiftTime+"");
    this.staff.gender = parseInt(this.staff.gender+"");
    this.staff.clinicID = parseInt(this.staff.clinicID+"");
    this.staff.role = parseInt(this.staff.role+"")
    this.staff.dob = new Date(this.staff.dobS)
    console.log("clinic",this.staff)
    if(!this.isUpdate) {

      this.staffService.create(undefined, this.staff)
        .subscribe({
          next:(r) => console.log("created, "+r),
          error: err => console.log("Err",err),
          complete : () => {
            console.log("Creation complete")
          }
        })
    }else {
      debugger
      this.staffService.update(undefined, this.staff)
      .subscribe({
        next:(r) => console.log("created, "+r),
        error: err => console.log("Err",err),
        complete : () => {
          console.log("updation complete")
        }
      })
    }

  }

  onChangeGender(event:any){
    this.staff.gender = event;
  }

  save(event : Event, formControlName : string) {
    debugger
    const file = (event!.target as HTMLInputElement).files![0];
    this.photo = file;
    this.filename = file.name;
    this.form.controls[formControlName].patchValue([file]);
    this.form.get(formControlName).updateValueAndValidity()
  }

  handleFileSelect(evt : any){
    var files = evt.target.files;
    var file = files[0];

  if (files && file) {
      var reader = new FileReader();
      this.filename = file.name;
      reader.onload =this._handleReaderLoaded.bind(this);
      reader.readAsBinaryString(file);
  }
}



_handleReaderLoaded(readerEvt : any) {
   var binaryString = readerEvt.target.result;
          this.staff.photoString= btoa(binaryString);
  }

  onChangeClinic(event :any) {
    console.log(event,"event")
    this.staff.clinicID = Number(event+"");
  }

  getDobString(datef:Date):string {
    var date = new Date(datef)
    console.log("date",date)
    var year = date.getFullYear()
    var day = date.getDate()+""
    var month = date.getMonth()+1+""
    console.log("month",month)
    console.log('day',day)
    if(Number(month) < 10){
      month = "0"+month
    }
    if(Number(day) < 10){
      day = "0"+day
    }
    return year+"-"+month+"-"+day
  }

  togglePWD(){
    this.notChangePassword = !this.notChangePassword;
  }

  toggleProfileImage(){
    this.notChangeImage = !this.notChangeImage;
  }

}
