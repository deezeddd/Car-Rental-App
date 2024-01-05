import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { ApiService } from 'src/app/services/api-service/api.service'

@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html',
  styleUrls: ['./add-car.component.css']
})
export class AddCarComponent {

  constructor(private apiService: ApiService, private toast: NgToastService, private route: Router) { }

  addCarForm = new FormGroup({
    maker: new FormControl('', [
      Validators.required,
      Validators.maxLength(100),
      Validators.pattern('^[a-zA-Z0-9 ]+$')
    ]),
    model: new FormControl('', [
      Validators.required,
      Validators.maxLength(100),
      Validators.pattern('^[a-zA-Z0-9 ]+$')
    ]),
    cost: new FormControl('', [
      Validators.required,
      Validators.pattern('^[0-9]+(\.[0-9]{1,2})?$'),
      Validators.min(0),
    ]),
    mileage: new FormControl('', [
      Validators.required,
      Validators.pattern('^[0-9]+(\.[0-9]{1,2})?$'),
      Validators.min(0),
      Validators.max(40)
    ]),
    available: new FormControl('Available',[])
  })

  isSubmitted = false;
  onSubmit() {
    const formData = (this.addCarForm);
    this.isSubmitted = true;

    if (this.addCarForm.valid) {
      this.apiService.addCar(formData.value).subscribe((res) => {
        // console.log("AddCar->", res);  //debug
        this.toast.success({ detail: "Successful", summary: "Car Added Successfully", duration: 3000 })
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
