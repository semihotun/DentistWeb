import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Disease } from '../models/Disease';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class DiseaseService {

  constructor(private httpClient: HttpClient) { }


  getDiseaseList(): Observable<Disease[]> {

    return this.httpClient.get<Disease[]>(environment.getApiUrl + '/diseases/getall')
  }

  getDiseaseById(id: number): Observable<Disease> {
    return this.httpClient.get<Disease>(environment.getApiUrl + '/diseases/getbyid?id='+id)
  }

  addDisease(disease: Disease): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/diseases/', disease, { responseType: 'text' });
  }

  updateDisease(disease: Disease): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/diseases/', disease, { responseType: 'text' });

  }

  deleteDisease(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/diseases/', { body: { id: id } });
  }


}