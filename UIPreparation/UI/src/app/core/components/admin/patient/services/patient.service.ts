import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Patient } from '../models/Patient';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class PatientService {

  constructor(private httpClient: HttpClient) { }


  getPatientList(): Observable<Patient[]> {

    return this.httpClient.get<Patient[]>(environment.getApiUrl + '/patients/getall')
  }

  getPatientById(id: number): Observable<Patient> {
    return this.httpClient.get<Patient>(environment.getApiUrl + '/patients/getbyid?id='+id)
  }

  addPatient(patient: Patient): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/patients/', patient, { responseType: 'text' });
  }

  updatePatient(patient: Patient): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/patients/', patient, { responseType: 'text' });

  }

  deletePatient(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/patients/', { body: { id: id } });
  }


}