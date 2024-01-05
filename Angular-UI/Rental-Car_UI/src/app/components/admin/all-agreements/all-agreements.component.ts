import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { BehaviorSubject } from 'rxjs';
import { ApiService } from 'src/app/services/api-service/api.service';
import { AuthService } from 'src/app/services/auth-service/auth.service';

@Component({
  selector: 'app-all-agreements',
  templateUrl: './all-agreements.component.html',
  styleUrls: ['./all-agreements.component.css']
})
export class AllAgreementsComponent implements OnInit {

  constructor(private toast: NgToastService,
    private apiService: ApiService, private authService: AuthService,
    private route: Router) { }

  agreements: any = [];
  Email: any;
  ngOnInit() {
    this.apiService.getAllRents().subscribe(res => {
      this.agreements = res;
      console.log(this.agreements);
    })
  }

  onDelete(id: any) {
    this.apiService.deleteRent(id).subscribe(res => {
    
    })
    window.location.reload()
  }

  onInspect(id: any) {
    this.apiService.inspection(id).subscribe(res => {
    })
    window.location.reload()
  }

}
