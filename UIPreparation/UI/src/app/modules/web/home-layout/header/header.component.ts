import { Component, ComponentFactoryResolver, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { loadComponent } from 'app/core/helper/dynamicComponent';
import { DynamicComponent } from 'app/core/utilities/dynamicComponent/dynamic.component';
import { DynamicComponentItem } from 'app/core/utilities/dynamicComponent/dynamiccomponent-item';
import { environment } from 'environments/environment';
import { Header1Component } from './headers/header1/header1.component';
import { Header2Component } from './headers/header2/header2.component';
import { HeaderService } from './services/header.service';

@Component({
  selector: 'app-header',
  template: ''
})
export class HeaderComponent implements OnInit {
  constructor(
    private componentFactoryResolver:ComponentFactoryResolver,
    private viewContainerRef:ViewContainerRef,
    private headerService :HeaderService
    ) { }

  ads: DynamicComponentItem;


  ngOnInit() {
    this.headerService.getTeplateSettings().subscribe(data=>{
      this.ads=this.headerService.getHeaders()[Number(data["header"])];
      loadComponent(this.ads,this.componentFactoryResolver,this.viewContainerRef);
    });
  }



}
