import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Clinic } from 'src/Models/Clinic';
import { Gender, ShiftTime } from 'src/Models/Enums';
import { Staff, StaffDTO } from 'src/Models/Staff';
import { AuthHttpService } from 'src/app/Services/HttpServices/AuthHttpService';
import { StaffHttpService } from 'src/app/Services/HttpServices/StaffHttpService';
import { StoreService } from 'src/app/Services/Store/StoreService';
import { NgSelectOption } from '@angular/forms';
import { ShiftTimeClass } from 'src/Models/ShiftTimeClass';
import { staffRowComponent } from './staffRow.component';
import { differenceInYears } from 'date-fns';
import { IfemptyPipe } from 'src/app/Pipes/ifempty.pipe';
import { Loading, Confirm, Notify } from 'notiflix';
import { delay, timeout } from 'rxjs';



@Component({
  selector: 'app-clinic',
  templateUrl: './clinic.component.html',
  styleUrls: ['./clinic.component.css']
})
export class ClinicComponent implements OnInit {
  shift !: string
  gender !: string
  age !: string
  staff !: Staff
  staffsTemp : StaffDTO[] = []
  clinic : Clinic = <Clinic>{}
  staffs : StaffDTO[] = []
  imageToShow : Map<number, any> = new Map<number, any>();
  isImageLoading !: boolean
  shifts : Array<ShiftTimeClass> = [{id:0,shift:'ALL'},{id:1, shift:'MORNING'}, {id:2, shift:'AFTERNOON'}, {id:3, shift:'NIGHT'}]
  shifts2 : Array<ShiftTimeClass> = [{id:0, shift:'MORNING'}, {id:1, shift:'AFTERNOON'}, {id:2, shift:'NIGHT'}]
  genders = ['MALE','FEMALE','OTHER']
  roles = ['Admin','Nurse','Doctor','Receptionist']
  shiftEnum !: ShiftTime
  shiftSelected : string = '0'
  page : number = 1;
  itemsToDisplay:number = 8;


  constructor(private router : Router, private staffService : StaffHttpService, private store : StoreService) {}

  ngOnInit(): void {
    if(this.store.clinic){
      this.clinic = this.store.clinic;
      this.getStaffs()
    }else {
      Notify.failure("Please select clinic first!")
      Loading.circle("Redirecting to Clinics page")
      setTimeout(()=> {
        Loading.remove()
        this.router.navigate(['/home'])
      }, 3000)

    }

  }

  details(staff : StaffDTO) {
    this.store.staff = staff;
    this.router.navigate(['staff'])
  }

  filterByShift(event : any) {
    if(event != null && event > 0){
      console.log("event",event);
      console.log("staff", this.staffs)
      this.staffsTemp = this.staffs.filter(x => x.shiftTime == event-1);
    }
    else {
      this.staffsTemp = this.staffs;
    }
    console.log("emp",this.staffsTemp)
  }

  consoleit(a : string) {
    console.log(a);
  }

  delete(id : number){

    Confirm.show(
      'Wait a Second',
      `Are you sure you want to remove this employee's details from database?`,
      'Yes',
      'No',
      () => {

        this.staffService.delete(undefined, id)
        .subscribe({
          next:()=>{
            this.staffs = this.staffs.filter(x => x.staffID != id);
            this.staffsTemp = this.staffs
          },
          error: err => console.log(err),
          complete: () => console.log("Deleted following staff with id : ", id)})
      },
      () => {
        Notify.warning("employee details not deleted")
        this.router.navigateByUrl('/home').then(
          () => {
            this.router.navigateByUrl('/clinic')
          }
        )
      },
      {
        okButtonBackground:'red',
        messageColor:'red',
      }
    )



  }

  remove(event : any) {
    debugger
    if(this.clinic){

      this.staffService.getAll(undefined,this.clinic.clinicId).subscribe(x =>
        {
          this.staffs = x;
          this.staffsTemp = this.staffs;
          console.log("stafftemp",this.staffsTemp, this.staffs);

        });
        this.shiftSelected = "0";
    }
  }

  pageChanged(event:any){
    this.page=event;
  }

  createImageFromBlob(image: Blob, id:number) {
    let reader = new FileReader();
    reader.addEventListener("load", () => {
       this.imageToShow.set(id, reader.result);
    }, false);

    if (image) {
       reader.readAsDataURL(image);
    }
  }
  setAll(){
    Loading.circle("Loading Images")
    this.staffs.forEach(x => {
      console.log("i am here", x)
      if(x != null) {
        this.staffService.getPhoto(undefined, x.fileName).subscribe(data => {
          this.createImageFromBlob(data, x.staffID);
          this.isImageLoading = true;
        }, error => {
          this.isImageLoading = false;
          console.log("error",error);
        });
      }
    })

    console.log("images",this.imageToShow)
    this.store.images = this.imageToShow;
    Loading.remove()
  }

  getStaffs(){
    this.clinic = this.store.clinic;

    if(this.clinic){
      this.staffService.getAll(undefined,this.clinic.clinicId).subscribe(x =>
        {
          this.staffs = x;
          this.staffsTemp = this.staffs;
          console.log("stafftemp",this.staffsTemp, this.staffs);
          this.setAll();
        });
    }
  }

  update(staff:Staff) {
    this.store.staff = staff;
    this.router.navigate(['/staff/update'])
  }

  changeShift(event:any, staff:StaffDTO){
    Confirm.show(
      'Wait a Second',
      'Are you sure you want to change working shift of this employee?',
      'Yes',
      'No',
      () => {
        staff.shiftTime = Number(event+"");
        this.staffService.updateShift(undefined, staff)
        .subscribe({next:()=>{},
        error: err => console.log(err), complete: () => console.log("Shift updated for staff", staff)})
      },
      () => {
        Notify.warning("Shift not changed")
        this.router.navigateByUrl('/home').then(
          () => {
            this.router.navigateByUrl('/clinic')
          }
        )
      }
    )
  }

}
