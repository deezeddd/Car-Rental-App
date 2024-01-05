import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { combineLatest } from 'rxjs';
import { ApiService } from 'src/app/services/api-service/api.service';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { UserStoreService } from 'src/app/services/user-store-service/user-store.service';

@Component({
  selector: 'app-rent-agreement',
  templateUrl: './rent-agreement.component.html',
  styleUrls: ['./rent-agreement.component.css']
})
export class RentAgreementComponent implements OnInit {
  Name: any;
  Email: any;
  constructor(private http: HttpClient, private toast: NgToastService,
    private apiService: ApiService, private authService: AuthService,
    private userStore: UserStoreService, private router: ActivatedRoute,
    private route: Router) { }

  addRentForm = new FormGroup({
    maker: new FormControl('', []),
    carId: new FormControl('', []),
    model: new FormControl('', []),
    rentalCost: new FormControl('', []),
    totalCost: new FormControl('', [Validators.pattern('^[0-9]+(\.[0-9]{1,2})?$'),]),
    available: new FormControl('Available', []), //TODO
    startDate: new FormControl('', []),
    endDate: new FormControl('', [Validators.required]),
    name: new FormControl('', []),
    email: new FormControl('', []),
  })


  car: any;
  id: any;
  totalCost: any = 0;
  currentDate: any;

  // this.currentDate = new Date();

  ngOnInit() {
    this.userDetails();
    this.addRentForm.get('email')?.setValue(this.Email);
    this.addRentForm.get('name')?.setValue(this.Name);

    console.log(this.Name);
    this.currentDate = new Date();
    this.router.paramMap.subscribe(params => {
      this.id = +params.get('carId')!;
      this.addRentForm.get('carId')?.setValue(this.id);

      if (this.id != null) {
        this.apiService.getCarById(this.id).subscribe(res => {
          this.car = (res);
          this.addRentForm.get('maker')?.setValue(this.car.maker);
          this.addRentForm.get('model')?.setValue(this.car.model);
          this.addRentForm.get('rentalCost')?.setValue(this.car.cost);
          this.addRentForm.get('startDate')?.setValue(this.currentDate);

          // this.addRentForm.get('mileage')?.setValue(this.car.mileage);

        })
      }
    })
  }
  userDetails() {
    combineLatest([                 //Rxjs property
      this.userStore.getFullNameFromStore(),
      this.userStore.getEmailFromStore()
    ]).subscribe(([fullNameFromStore, emailFromStore]) => {
      const fullNameFromToken = this.authService.getFullNameFromToken();
      const emailFromToken = this.authService.getEmailFromToken();
      this.Name = fullNameFromStore || fullNameFromToken;
      this.Email = emailFromStore || emailFromToken;
    });
  }

  duration: any
  //Date Change -> Cost Calculated
  onChange() {

    this.duration = this.addRentForm.get('endDate')?.value
    var substractDate = new Date(this.duration).getDate() - new Date().getDate();
    this.totalCost = parseFloat((this.car.cost * substractDate).toFixed(2))
    if (this.totalCost <= 0) {
      this.totalCost = NaN;
      alert("select future date")
    }
    else {
      this.addRentForm.get('totalCost')?.setValue(this.totalCost);
    }

  }

  isSubmitted = false;
  onSubmit() {
    const formData = (this.addRentForm);
    console.log(formData.value);
    this.isSubmitted = true;

    if (this.addRentForm.valid) {
      this.apiService.addRent(formData.value).subscribe((res) => {
        console.log("AddRent->", res);  //debug
        this.toast.success({ detail: "Successful", summary: "Agreement Added Successfully", duration: 3000 })
        this.route.navigate([''])
      }, err => (
        this.toast.error({ detail: "Unsuccessful", summary: "Please add correct details", duration: 3000 })
      )
      );

    } else {
      alert('Form validation failed');
    }
  }

}