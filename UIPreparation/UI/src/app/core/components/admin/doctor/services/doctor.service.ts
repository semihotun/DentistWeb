import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Doctor } from '../models/Doctor';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  constructor(private httpClient: HttpClient) { }


  getDoctorList(): Observable<Doctor[]> {

    return this.httpClient.get<Doctor[]>(environment.getApiUrl + '/doctors/getall')
  }

  getDoctorById(id: number): Observable<Doctor> {
    return this.httpClient.get<Doctor>(environment.getApiUrl + '/doctors/getbyid?id='+id)
  }

  addDoctor(doctor: Doctor): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/doctors/', doctor, { responseType: 'text' });
  }

  updateDoctor(doctor: Doctor): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/doctors/', doctor, { responseType: 'text' });

  }

  deleteDoctor(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/doctors/', { body: { id: id } });
  }


}