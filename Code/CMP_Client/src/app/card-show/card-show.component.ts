import { Component, OnInit } from '@angular/core';
import { Clinic } from 'src/Models/Clinic';
import { ClinicHttpService } from 'src/app/Services/HttpServices/ClinicHttpService';
import { StoreService } from 'src/app/Services/Store/StoreService';

@Component({
  selector: 'app-card-show',
  templateUrl: './card-show.component.html',
  styleUrls: ['./card-show.component.css']
})
export class CardShowComponent implements OnInit {
  clinics : Clinic[] = []
  constructor(private service : ClinicHttpService, private store:StoreService) { }

  ngOnInit(): void {
    if(this.store.clinics == null){
      this.service.getAll().subscribe(x => {
        this.clinics = x;
        this.store.clinics = x;
      })
    }else{
      this.clinics = this.store.clinics;
    }

  }

}
