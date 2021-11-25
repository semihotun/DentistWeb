import { Component, ComponentFactoryResolver, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { loadComponent } from 'app/core/helper/dynamicComponent';
import { DynamicComponent } from 'app/core/utilities/dynamicComponent/dynamic.component';
import { DynamicComponentItem } from 'app/core/utilities/dynamicComponent/dynamiccomponent-item';
import { environment } from 'environments/environment';
import { Header1Component } from './headers/header1/header1.component';
import { Header2Component } from './headers/header2/header2.component';

@Component({
  selector: 'app-header',
  template: ''
})
export class HeaderComponent implements OnInit {
  constructor(private componentFactoryResolver:ComponentFactoryResolver,private viewContainerRef:ViewContainerRef) { }
  ads: DynamicComponentItem;

  getHeaders() {
    return [
      new DynamicComponentItem(Header1Component,{}),
      new DynamicComponentItem(Header2Component,{}),
    ];
  }

  ngOnInit() {
    this.ads=this.getHeaders()[Number(environment.getTemplateSetting.header)];
    loadComponent(this.ads,this.componentFactoryResolver,this.viewContainerRef);
  }



}
