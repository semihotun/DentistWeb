import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { Currency } from 'app/models/currency';
import { LookUp } from 'app/core/models/lookUp';


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

  getCurrencyLookup():Observable<LookUp[]>
  {
     return this.httpClient.get<LookUp[]>(environment.getApiUrl + "/currencies/getcurrencylookUp")
  }

}