import { Gender, ShiftTime } from "./Enums";

export interface Staff {
  staffID : number
  name : string
  shiftTime : number
  dob : Date
  phone : number
  gender : number
  role : number
  photoString : string
  photo : File
  fileName : string
  password : string
  clinicID : number
  dobS : string
}

export class StaffDTO {
  staffID !: number
  name !: string
  shiftTime !: number
  dob !: Date
  phone !: number
  gender !: number
  role !: number
  photoString !: string
  photo !: File
  fileName !: string
  password !: string
  clinicID !: number
  dobS !: string
}

