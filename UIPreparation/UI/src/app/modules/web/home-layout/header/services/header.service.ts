import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';
import { Disease } from 'app/models/disease';
import { LookUp } from 'app/core/models/lookUp';
import { Header1Component } from '../headers/header1/header1.component';
import { Header2Component } from '../headers/header2/header2.component';
import { DynamicComponentItem } from 'app/core/utilities/dynamicComponent/dynamiccomponent-item';
import { TemplateSetting } from 'app/models/templateSetting';


@Injectable({
  providedIn: 'root'
})
export class HeaderService {

  constructor(private httpClient: HttpClient) { }

  getHeaders() {
    return [
      new DynamicComponentItem(Header1Component,{}),
      new DynamicComponentItem(Header2Component,{}),
    ];
  }
  
  getTeplateSettings(): Observable<object> {
    return this.httpClient.get<object>(environment.getApiUrl + '/templateSetting/getjson')
  }

  updateTemplateSetting(templateSetting: TemplateSetting): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/templateSetting/', templateSetting, { responseType: 'text' });
  }
  
}
