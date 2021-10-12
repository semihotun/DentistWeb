import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Doctor } from '../models/Doctor';
import { environment } from 'environments/environment';
import { formData } from 'app/core/defines/formData';


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
    let data=formData(doctor);
    return this.httpClient.post(environment.getApiUrl + '/doctors/', data, { responseType: 'text' });
  }

  updateDoctor(doctor: Doctor): Observable<any> {
    let data=formData(doctor);
      
    return this.httpClient.put(environment.getApiUrl + '/doctors/', data, { responseType: 'text' });  
  }

  deleteDoctor(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/doctors/', { body: { id: id } });
  }


}