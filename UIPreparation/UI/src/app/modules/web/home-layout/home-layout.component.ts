import { Component, OnInit } from '@angular/core';
import { DynamicComponentItem } from 'app/core/utilities/dynamicComponent/dynamiccomponent-item';
import { environment } from 'environments/environment';
import { Header1Component } from './header/headers/header1/header1.component';
import { Header2Component } from './header/headers/header2/header2.component';


@Component({
  selector: 'app-home-layout',
  templateUrl: './home-layout.component.html',
  styleUrls: ['./home-layout.component.css']
})
export class HomeLayoutComponent implements OnInit {

  constructor() {}

  ngOnInit() {
   
  }


}
