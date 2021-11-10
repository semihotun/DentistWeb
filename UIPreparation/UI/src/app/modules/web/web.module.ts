import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { RouterModule, Routes } from '@angular/router';
import { HomeLayoutComponent } from './home-layout/home-layout.component';


export const WebRoutes: Routes = [
  { path: '', component: HomeComponent}, 
  { path:'homelayout',component:HomeLayoutComponent}
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(WebRoutes)
  ],
  declarations: [HomeComponent,HomeLayoutComponent]
})
export class WebModule { }



