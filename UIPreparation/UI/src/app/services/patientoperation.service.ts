import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { PatientOperation } from 'app/models/patientOperation';
import { PatientOperationDto } from 'app/models/dto/PatientOperationDTO';


@Injectable({
  providedIn: 'root'
})
export class PatientOperationService {

  constructor(private httpClient: HttpClient) { }


  getPatientOperationList(): Observable<PatientOperation[]> {

    return this.httpClient.get<PatientOperation[]>(environment.getApiUrl + '/patientOperations/getall')
  }

  getPatientOperationById(id: number): Observable<PatientOperation> {
    return this.httpClient.get<PatientOperation>(environment.getApiUrl + '/patientOperations/getbyid?id='+id)
  }

  addPatientOperation(patientOperation: PatientOperation): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/patientOperations/', patientOperation, { responseType: 'text' });
  }

  updatePatientOperation(patientOperation: PatientOperation): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/patientOperations/', patientOperation, { responseType: 'text' });

  }

  deletePatientOperation(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/patientOperations/', { body: { id: id } });
  }

  getPatientOperationDtoByPatientId(patientId:number): Observable<PatientOperationDto[]> 
  {
    return this.httpClient.get<PatientOperationDto[]>(environment.getApiUrl + '/patientOperations/getpatientoperationdtobypatientId?patientId='+patientId);
  }

}