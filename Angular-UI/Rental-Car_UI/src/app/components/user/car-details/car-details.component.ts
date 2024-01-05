import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { ApiService } from 'src/app/services/api-service/api.service';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { UserStoreService } from 'src/app/services/user-store-service/user-store.service';

@Component({
  selector: 'app-car-details',
  templateUrl: './car-details.component.html',
  styleUrls: ['./car-details.component.css']
})
export class CarDetailsComponent implements OnInit {

  constructor(
    private apiService: ApiService,
    private router: ActivatedRoute,
    private route: Router) { }

  car: any;
  id: any;
  totalCost: any = 0;
  currentDate: any;
  duration = new FormControl();
  onChange() {
    var substractDate = new Date(this.duration.value).getDate() - new Date().getDate();
    this.totalCost = parseFloat((this.car.cost * substractDate).toFixed(2))
    if(this.totalCost <= 0){
      this.totalCost = NaN;
    }
  }
  ngOnInit() {
    this.currentDate = new Date();

    this.router.paramMap.subscribe(params => {
      this.id = +params.get('id')!;
      if (this.id != null) {
        this.apiService.getCarById(this.id).subscribe(res => {
          // console.log(res);
          // next : res
          this.car = (res);
        })
      }
    })
  }

  navigateTo(id: any) {
    this.route.navigate(['/agreement/', id])
  }



}
