import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RentAgreementComponent } from './components/user/rent-agreement/rent-agreement.component';
import { MyAgreementsComponent } from './components/user/my-agreements/my-agreements.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TokenInterceptor } from './Interceptors/token.interceptor';
import { NgToastModule } from 'ng-angular-popup';
import { AddCarComponent } from './components/admin/add-car/add-car.component';
import { EditCarComponent } from './components/admin/edit-car/edit-car.component';
import { AllAgreementsComponent } from './components/admin/all-agreements/all-agreements.component';
import { CarDetailsComponent } from './components/user/car-details/car-details.component';
import { authGuard } from './guard/auth.guard';
import { RoleGuard } from './guard/role.guard';
import { LoggedInGuard } from './guard/logged-in.guard';


const appRoutes: Routes = [
  {
    path: '',
    component: HomeComponent,

  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [LoggedInGuard]

  },
  {
    path: 'my-agreements',
    component: MyAgreementsComponent,
    canActivate:[authGuard]

  },
  {
    path: 'car-details/:id',
    component: CarDetailsComponent,
    canActivate:[authGuard]

  },
  {
    path: 'agreement/:carId',
    component: RentAgreementComponent,
    canActivate:[authGuard]

  },
  {
    path: 'add-car',
    component: AddCarComponent,
    canActivate: [RoleGuard]

  },
  {
    path: 'edit-car/:id',
    component: EditCarComponent,
    canActivate: [RoleGuard]

  },
  {
    path: 'all-agreements',
    component: AllAgreementsComponent,
    canActivate: [RoleGuard]

  },
]

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RentAgreementComponent,
    MyAgreementsComponent,
    AddCarComponent,
    EditCarComponent,
    CarDetailsComponent,
    AllAgreementsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    NgToastModule,
    RouterModule.forRoot(appRoutes)
  ],

  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass : TokenInterceptor,
    multi: true
  }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
