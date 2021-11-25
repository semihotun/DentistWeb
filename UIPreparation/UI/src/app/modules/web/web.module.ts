import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { RouterModule, Routes } from '@angular/router';
import { HomeLayoutComponent } from './home-layout/home-layout.component';
import { HeaderComponent } from './home-layout/header/header.component';
import { Header1Component } from './home-layout/header/headers/header1/header1.component';
import { Header2Component } from './home-layout/header/headers/header2/header2.component';

export const WebRoutes: Routes = [
  { path: '', component: HomeComponent}, 
  { path:'homelayout',component:HomeLayoutComponent}
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(WebRoutes)
  ],
  declarations: [
    HomeComponent,
    HomeLayoutComponent,
    HeaderComponent,
    Header1Component,
    Header2Component]
})
export class WebModule { }



