import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { DoctorType } from 'app/models/doctorType';
import { LookUp } from 'app/core/models/lookUp';


@Injectable({
  providedIn: 'root'
})
export class DoctorTypeService {

  constructor(private httpClient: HttpClient) { }


  getDoctorTypeList(): Observable<DoctorType[]> {

    return this.httpClient.get<DoctorType[]>(environment.getApiUrl + '/doctorTypes/getall')
  }

  getDoctorTypeById(id: number): Observable<DoctorType> {
    return this.httpClient.get<DoctorType>(environment.getApiUrl + '/doctorTypes/getbyid?id='+id)
  }

  addDoctorType(doctorType: DoctorType): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/doctorTypes/', doctorType, { responseType: 'text' });
  }

  updateDoctorType(doctorType: DoctorType): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/doctorTypes/', doctorType, { responseType: 'text' });

  }

  deleteDoctorType(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/doctorTypes/', { body: { id: id } });
  }

  getDoctorTypeLookup():Observable<LookUp[]>
  {
     return this.httpClient.get<LookUp[]>(environment.getApiUrl + "/doctortypes/getdoctortypelookUp")
  }
}