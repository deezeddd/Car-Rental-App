import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { Observable, combineLatest } from 'rxjs';
import { ApiService } from 'src/app/services/api-service/api.service';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { UserStoreService } from 'src/app/services/user-store-service/user-store.service';

@Component({
  selector: 'app-my-agreements',
  templateUrl: './my-agreements.component.html',
  styleUrls: ['./my-agreements.component.css']
})
export class MyAgreementsComponent implements OnInit {

  constructor(private toast: NgToastService,
    private apiService: ApiService, private authService: AuthService,
    private userStore: UserStoreService, private router: ActivatedRoute,
    private route: Router, private changeDetectorRef: ChangeDetectorRef) { }

  agreements: any = [];
  Email: any;
  ngOnInit() {
    this.userDetails()
    this.apiService.getRentByEmail(this.Email).subscribe(res => {
      this.agreements = res;
      console.log(this.agreements);
    })
  }
  userDetails() {
    combineLatest([                 //Rxjs property
      this.userStore.getFullNameFromStore(),
      this.userStore.getEmailFromStore()
    ]).subscribe(([fullNameFromStore, emailFromStore]) => {
      const fullNameFromToken = this.authService.getFullNameFromToken();
      const emailFromToken = this.authService.getEmailFromToken();
      this.Email = emailFromStore || emailFromToken;
    });
  }

  onReturn(id: any,) {
    this.apiService.reqForReturn(id).subscribe(res => {
    });
    
  }

}
