import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { AppointmentTime } from 'app/models/appointmentTime';


@Injectable({
  providedIn: 'root'
})
export class AppointmentTimeService {

  constructor(private httpClient: HttpClient) { }


  getAppointmentTimeList(): Observable<AppointmentTime[]> {

    return this.httpClient.get<AppointmentTime[]>(environment.getApiUrl + '/appointmentTimes/getall')
  }

  getAppointmentTimeById(id: number): Observable<AppointmentTime> {
    return this.httpClient.get<AppointmentTime>(environment.getApiUrl + '/appointmentTimes/getbyid?id='+id)
  }

  addAppointmentTime(appointmentTime: AppointmentTime): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/appointmentTimes/', appointmentTime, { responseType: 'text' });
  }

  updateAppointmentTime(appointmentTime: AppointmentTime): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/appointmentTimes/', appointmentTime, { responseType: 'text' });

  }

  deleteAppointmentTime(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/appointmentTimes/', { body: { id: id } });
  }


}