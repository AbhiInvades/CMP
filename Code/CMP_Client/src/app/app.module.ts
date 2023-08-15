import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { JwtModule, JWT_OPTIONS} from '@auth0/angular-jwt';
import { NavbarComponent } from './shared/Layout/navbar/navbar.component';
import { AuthHttpService } from 'src/app/Services/HttpServices/AuthHttpService';
import { ClinicHttpService } from 'src/app/Services/HttpServices/ClinicHttpService';
import { StaffHttpService } from 'src/app/Services/HttpServices/StaffHttpService';
import { LoginComponent } from './shared/login/login.component';
import { CommonModule } from '@angular/common';
import {FormsModule} from '@angular/forms';
import { HomeComponent } from './home/home.component';
import { ClinicComponent } from './clinic/view-clinic/clinic.component';
import { StoreService } from 'src/app/Services/Store/StoreService';
import { AuthInterceptor } from 'src/Interceptor/AuthInterceptor';
import {saveAs} from 'file-saver';
import * as FileSaver from 'file-saver';
import { NewClinicComponent } from './clinic/new-clinic/new-clinic.component';
import { NewStaffComponent } from './staff/new-staff/new-staff.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ConsolePipe } from './Pipes/console.pipe';
import { staffRowComponent } from './clinic/view-clinic/staffRow.component';
import { StaffComponent } from './staff/view-staff/staff.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import {MatTableModule} from '@angular/material/table';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input'
import {NgxPaginationModule} from 'ngx-pagination';
import { IfemptyPipe } from './Pipes/ifempty.pipe';
import { DobToAgePipe } from './Pipes/dob-to-age.pipe';
import { IfnullPipe } from './Pipes/ifnull.pipe';
import { LandingComponent } from './landing/landing.component';
import { CardComponent } from './card/card.component';
import { CardShowComponent } from './card-show/card-show.component';
import { FeaturesComponent } from './features/features.component';
import { ReviewsComponent } from './reviews/reviews.component';
import { FooterComponent } from './footer/footer.component';
import { CookieService } from 'ngx-cookie-service';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ConsolePipe,
    LoginComponent,
    NavbarComponent,
    staffRowComponent,
    StaffComponent,
    NewStaffComponent,
    NewClinicComponent,
    ClinicComponent,
    IfnullPipe,
    IfemptyPipe,
    DobToAgePipe,
    LandingComponent,
    CardComponent,
    CardShowComponent,
    FeaturesComponent,
    ReviewsComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatButtonModule,
    MatTableModule,
    MatInputModule,
    MatIconModule,
    NgxPaginationModule,
    // JwtModule.forRoot({
    //   config: {
    //     tokenGetter: tokenGetter,
    //     allowedDomains: ['localhost:3000', 'example.com'],
    //     disallowedRoutes: ["http://example.com/examplebadroute/"],
    //     authScheme: "Bearer " // Default value
    //   }
    // }),
    BrowserAnimationsModule
  ],
  providers: [AuthHttpService, ClinicHttpService, StaffHttpService, StoreService, CookieService,
    {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent],
  schemas : [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
