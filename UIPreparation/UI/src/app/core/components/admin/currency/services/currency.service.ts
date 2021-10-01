import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Currency } from '../models/Currency';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class CurrencyService {

  constructor(private httpClient: HttpClient) { }


  getCurrencyList(): Observable<Currency[]> {

    return this.httpClient.get<Currency[]>(environment.getApiUrl + '/currencies/getall')
  }

  getCurrencyById(id: number): Observable<Currency> {
    return this.httpClient.get<Currency>(environment.getApiUrl + '/currencies/getbyid?id='+id)
  }

  addCurrency(currency: Currency): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/currencies/', currency, { responseType: 'text' });
  }

  updateCurrency(currency: Currency): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/currencies/', currency, { responseType: 'text' });

  }

  deleteCurrency(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/currencies/', { body: { id: id } });
  }


}