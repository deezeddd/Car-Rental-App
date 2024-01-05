import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { combineLatest } from 'rxjs';
import { ApiService } from 'src/app/services/api-service/api.service';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { UserStoreService } from 'src/app/services/user-store-service/user-store.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private http: HttpClient, private toast: NgToastService,
    private apiService: ApiService, private authService: AuthService,
    private userStore: UserStoreService,
     private route : Router) { }
  ngOnInit(): void {
    this.getCarsList();
    this.userDetails();
  }

  originalCars: any = [];
  cars:any = []
  getCarsList() {
    this.apiService.getAllCars().subscribe((data: any) => {
      // console.log(data);
      this.cars = data   // for search purpose
      this.originalCars = data;  //stores permanent list
    });
  }

  onClick(id:any){
    this.route.navigate(["car-details",id])
  }
  onEdit(id:any){
    console.log(id);
    this.route.navigate(["edit-car",id])
  }
  onDelete(id:any){
    this.apiService.deleteCar(id).subscribe((res)=>{
      console.log("Deleted");
    })
  }
  isLoggedIn: any
  Name: any;
  Role: any;
  isAdmin = false;

  userDetails() {
    combineLatest([                 //Rxjs property
      this.userStore.getFullNameFromStore(),
      this.userStore.getRoleFromStore()
    ]).subscribe(([fullNameFromStore, roleFromStore]) => {
      const fullNameFromToken = this.authService.getFullNameFromToken();
      const roleFromToken = this.authService.getRoleFromToken();

      // console.log("Navbar name -> ", fullNameFromStore);
      // console.log("Navbar role -> ", roleFromStore);

      this.Name = fullNameFromStore || fullNameFromToken;
      this.Role = roleFromStore || roleFromToken;

      if (this.Role === "Admin") {
        this.isAdmin = true;
      } else {
        this.isAdmin = false;
      }
    });
  }

  public maker:any = null;
  public model:any = null;
  public cost:any = null;
  public filter:any = null;



  filterByModel(){
    this.apiService.filterByModel(this.model).subscribe(res=>{
      this.cars = res

    })
  }
  filterByMaker(){
    this.apiService.filterByMaker(this.maker).subscribe(res=>{
      console.log("maker",res);
      this.cars = res
    })
    // console.log(this.maker);
  }
  filterByCost(){
    this.apiService.filterByCost(this.cost).subscribe(res=>{
      this.cars = res
    })
  }
  reset(){
    this.cars = this.originalCars
   this.filter = "null";
    this.maker = null;
    this.model= null;
    this.cost = null;
  }

}
