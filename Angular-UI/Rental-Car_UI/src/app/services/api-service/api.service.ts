import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl: String = "https://localhost:7142/api/"
  constructor(private http: HttpClient) { }

  getAllCars(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}GetAllCars`)
  }
  addCar(carObj: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}AddCar`, carObj)
  }
  editCar(id: any, carObj: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}EditCar/${id}`, carObj)
  }
  deleteCar(id: any): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}DeleteCar/${id}`)
  }
  getCarById(id: any): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}GetCarById/${id}`)
  }
  filterByMaker(maker: any): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}FilterByMaker?Maker=${maker}`)
  }
  filterByModel(model: any): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}FilterByModel?Model=${model}`)
  }
  filterByCost(cost: any): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}FilterByCost?Cost=${cost}`)
  }

  getAllRents(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}GetAllRents`)
  }
  addRent(rentObj: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}AddRent`, rentObj)
  }
  editRent(id: any, rentObj: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}EditRent/${id}`, rentObj)
  }
  deleteRent(id: any): Observable<any> {
    return this.http.delete<any>(`${this.baseUrl}DeleteRent/${id}`)
  }
  getRentById(id: any): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}GetRentById/${id}`)
  }
  getRentByEmail(email: any): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}GetRentByEmail?email=${email}`)
  }
  reqForReturn(id: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}ReqForReturn?id=${id}`, id)
  }
  inspection(id: any): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}Inspection?id=${id}`, id)
  }


}
