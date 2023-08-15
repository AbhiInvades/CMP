import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './Guards/auth.guard';
import { ClinicComponent } from './clinic/view-clinic/clinic.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './shared/login/login.component';
import { NewClinicComponent } from './clinic/new-clinic/new-clinic.component';
import { NewStaffComponent } from './staff/new-staff/new-staff.component';
import { StaffComponent } from './staff/view-staff/staff.component';
import { LandingComponent } from './landing/landing.component';

const routes: Routes = [
  {path:"login", component:LoginComponent},
  {path:"", component:LandingComponent},
  {path:"home",component:HomeComponent, canActivate:[AuthGuard]},
  {path:"clinic", component:ClinicComponent, canActivate:[AuthGuard]},
  {path:"clinic/:mode", component:NewClinicComponent, canActivate:[AuthGuard]},
  {path:"staff", component:StaffComponent, canActivate:[AuthGuard]},
  {path:"staff/:mode", component:NewStaffComponent, canActivate:[AuthGuard]},
  {path:"landing", component:LandingComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
