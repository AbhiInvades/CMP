import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Clinic } from 'src/Models/Clinic';
import { ClinicHttpService } from 'src/app/Services/HttpServices/ClinicHttpService';
import {Router} from '@angular/router'
import { AuthHttpService } from 'src/app/Services/HttpServices/AuthHttpService';
import { StoreService } from 'src/app/Services/Store/StoreService';
import { CommonModule } from '@angular/common';
import { tap } from 'rxjs';
import { Confirm, Notify } from 'notiflix';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
  {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
  {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
  {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'},
  {position: 5, name: 'Boron', weight: 10.811, symbol: 'B'},
  {position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C'},
  {position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N'},
  {position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O'},
  {position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F'},
  {position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne'},
];

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit {
  clinics : Clinic[] = [<Clinic>{}]
  page : number = 1;
  itemsToDisplay:number = 8;
  columns = [
    {
      columnDef: 'name',
      header: 'Name.',
      cell: (element: Clinic) => `${element.name}`,
    },
    {
      columnDef: 'address',
      header: 'Address',
      cell: (element: Clinic) => `${element.address}`,
    },
    {
      columnDef: 'department',
      header: 'Department',
      cell: (element: Clinic) => `${element.department}`,
    },
    {
      columnDef: 'telephone',
      header: 'Telephone',
      cell: (element: Clinic) => `${element.telephone}`,
    },
  ];
  dataSource = this.clinics;
  displayedColumns = ['name', 'address','department','telephone','actions']
  departments = ['Cardiology', 'Dermatology','Neorology','Psychology']
  isLoaded : boolean = false;
  constructor(private clinicService : ClinicHttpService, private router : Router, private authService : AuthHttpService, private store : StoreService) {

    if(!this.authService.isLoggedIn()){
      this.router.navigate(['login'])
    }else {
      this.clinicService.getAll().subscribe(res => {
        this.clinics = res;
        this.dataSource = this.clinics;
        this.displayedColumns = ['name', 'address','department','telephone','view','update','delete']//this.columns.map(c => c.columnDef);
        this.isLoaded = true;
        this.store.clinics = this.clinics;
        console.log(res)
        console.log(this.clinics)
      }, e => {
        console.log(e)
      })
    }
  }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {


  }

  log(s : any) {
    console.log("logging",s)
  }

  details(clinic : Clinic) : void {
    this.router.navigate(['clinic'])
    this.store.clinic = clinic;
  }

  delete(id : number) {
    debugger
    Confirm.show(
      'Wait a Second',
      `Are you sure you want to remove `+this.clinics.find(x => x.clinicId == id)?.name+`'s details from database?`,
      'Yes',
      'No',
      () => {

        this.clinicService.delete(undefined, id)
        .subscribe({
          next:()=>{
            this.clinics = this.clinics.filter(x => x.clinicId != id)
            this.dataSource = this.clinics
          },
          error: err => console.log(err),
          complete: () => console.log("Deleted following staff with id : ", id)})
      },
      () => {
        Notify.warning("clinic details not deleted")
        this.router.navigateByUrl('/landing').then(
          () => {
            this.router.navigateByUrl('/home')
          }
        )
      },
      {
        okButtonBackground:'red',
        messageColor:'red',
      }

    )

  }

  update(clinic:Clinic) {
    this.store.clinic = clinic;
    this.router.navigate(['clinic/update'])
  }

  pageChanged(event:any){
    this.page=event;
  }

}
