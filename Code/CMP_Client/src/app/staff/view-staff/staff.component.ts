import { Component, OnInit } from '@angular/core';
import { debug } from 'console';
import { differenceInCalendarYears, differenceInYears, parseISO } from 'date-fns';
import { saveAs } from 'file-saver';
import { ShiftTimeClass } from 'src/Models/ShiftTimeClass';
import { Staff } from 'src/Models/Staff';
import { StaffHttpService } from 'src/app/Services/HttpServices/StaffHttpService';
import { StoreService } from 'src/app/Services/Store/StoreService';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-staff',
  templateUrl: './staff.component.html',
  styleUrls: ['./staff.component.css'],
})
export class StaffComponent implements OnInit {
  photo !: string
  staff !: Staff
  age !: string
  imageToShow !: any
  isImageLoading !: boolean
  shiftSelected !: number
  gender = ['MALE','FEMALE', 'OTHER']
  shifts  = ['MORNING', 'AFTERNOON', 'NIGHT']
  isChangeShift : boolean = false;

  constructor(private store : StoreService, private staffService : StaffHttpService) {
    this.staff = store.staff
    console.log("staffcomponent:23",this.staff)
    if(this.staff) {
      this.shiftSelected = this.staff.shiftTime;
      this.age = differenceInYears(new Date(), new Date(this.staff.dob))+" years";
      staffService.getPhoto(undefined, this.staff.fileName).subscribe(data => {
        this.createImageFromBlob(data);
        this.isImageLoading = false;
      }, error => {
        this.isImageLoading = false;
        console.log(error);
      });
    }

  }

  ngOnInit(): void {
  }

  createImageFromBlob(image: Blob) {
    let reader = new FileReader();
    debugger
    reader.addEventListener("load", () => {
       this.imageToShow = reader.result;
    }, false);

    if (image) {
       reader.readAsDataURL(image);
    }
  }

  onChangeShift(event:any) {
    debugger

  }

  changeShift(){
    this.isChangeShift = true;
  }

}
