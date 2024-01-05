import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { ApiService } from 'src/app/services/api-service/api.service';

@Component({
  selector: 'app-edit-car',
  templateUrl: './edit-car.component.html',
  styleUrls: ['./edit-car.component.css']
})
export class EditCarComponent implements OnInit {

  constructor(private apiService: ApiService, private toast: NgToastService, private route: Router,private router: ActivatedRoute) { }

  editCarForm = new FormGroup({
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
  })

  car: any;
  id: any;
  ngOnInit(): void {
    this.router.paramMap.subscribe(params => {
      this.id = +params.get('id')!;
      if (this.id != null) {
        this.apiService.getCarById(this.id).subscribe(res => {
          console.log(res);
          this.car = res;
          this.editCarForm.get('maker')?.setValue(this.car.maker);
          this.editCarForm.get('model')?.setValue(this.car.model);
          this.editCarForm.get('cost')?.setValue(this.car.cost);
          this.editCarForm.get('mileage')?.setValue(this.car.mileage);
        })
      }
    })
  }

  isSubmitted = false;
  onSubmit() {
    const formData = (this.editCarForm);
    this.isSubmitted = true;

    if (this.editCarForm.valid) {
      const updatedCar = { ...this.editCarForm.value }; // Create a copy of the form values
      console.log(updatedCar);  //debug

      this.apiService.editCar(this.id,updatedCar).subscribe((res) => {
        this.toast.success({ detail: "Successful", summary: "Car Edited Successfully", duration: 3000 })
        this.route.navigate([''])
      }, (err) => (
        this.toast.error({ detail: "Unsuccessful", summary: "Please add correct details", duration: 3000 })
      )
      );

    } else {
      alert('Form validation failed');
    }
  }
}
