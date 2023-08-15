import { Injectable } from "@angular/core";
import { Clinic } from "src/Models/Clinic";
import { Staff } from "src/Models/Staff";


Injectable ({
  providedIn : 'root'
})
export class StoreService {
  clinic !: Clinic
  staff !: Staff
  clinics !: Clinic[]
  images !: Map<number, any>
  title !: string
  constructor(){
  }
}
