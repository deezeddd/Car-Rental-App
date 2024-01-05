import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import 'bootstrap'
import { NgToastService } from 'ng-angular-popup';
import { combineLatest } from 'rxjs';
import { AuthService } from './services/auth-service/auth.service';
import { UserStoreService } from './services/user-store-service/user-store.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Rental-Car_UI';

  isLoggedIn: any
  Name: any;
  Role: any;
  Email:any
  isAdmin = false;
  constructor(private route: Router, private authService: AuthService,
    private toast: NgToastService, private userStore: UserStoreService,
  ) { }

  ngOnInit(): void {
    // var a = localStorage.getItem('token')
    // console.log("init -> ", a);
    this.isLoggedIn = this.authService.isLoggedIn();

    this.userDetails();

    this.authService.loggedIn$.subscribe((loggedIn: boolean) => {
      console.log("navbar LoggedinStatus:-> ", loggedIn)
      this.isLoggedIn = loggedIn;
    });
  }

  userDetails() {
    combineLatest([                 //Rxjs property
      this.userStore.getFullNameFromStore(),
      this.userStore.getRoleFromStore(),
      this.userStore.getEmailFromStore()

    ]).subscribe(([fullNameFromStore, roleFromStore,emailFromStore]) => {
      const fullNameFromToken = this.authService.getFullNameFromToken();
      const roleFromToken = this.authService.getRoleFromToken();
      const emailFromToken = this.authService.getEmailFromToken();


      // console.log("Navbar name -> ", fullNameFromStore);
      // console.log("Navbar role -> ", roleFromStore);

      this.Name = fullNameFromStore || fullNameFromToken;
      this.Role = roleFromStore || roleFromToken;
      this.Email = emailFromStore|| emailFromToken;


      if (this.Role === "Admin") {
        this.isAdmin = true;
      } else {
        this.isAdmin = false;
      }
    });
  }

  signOut() {
    this.Name = "";
    this.isAdmin = false;
    this.Role = null;
    this.authService.signOut();
    this.toast.success({ detail: 'Success', summary: "Sign Out Successful", duration: 3000, });
    this.route.navigate(['login'])
  }

}
