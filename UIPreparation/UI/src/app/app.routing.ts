import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';
import { AdminLayoutComponent } from './core/components/app/layouts/admin-layout/admin-layout.component';
import { HomeComponent } from './modules/web/home/home.component';
import { HomeLayoutComponent } from './modules/web/home-layout/home-layout.component';
// import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
// import { LoginComponent } from './login/login.component';

const routes: Routes = [
  // {
  //   path: '',
  //   redirectTo: 'home',
  //   pathMatch: 'full',
  // },
  {
    path: '',
    component: HomeLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: './modules/web/web.module#WebModule'
      }
    ]
  },

  {
    path: 'admin',
    component: AdminLayoutComponent,
    children: [
      {
        path: '',
        // loadChildren: './core/components/app/layouts/admin-layout/admin-layout.module#AdminLayoutModule'
        loadChildren: './core/modules/admin-layout.module#AdminLayoutModule'
      }
    ]
  }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
    [RouterModule]
  ],
})
export class AppRoutingModule { }
