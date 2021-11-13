import { HostListener } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../../admin/login/services/auth.service';


declare const $: any;
declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
    claim:string;
}
export const ADMINROUTES: RouteInfo[] = [
  { path: '/admin/user', title: 'Users', icon: 'how_to_reg', class: '', claim:"GetUsersQuery" },
  { path: '/admin/group', title: 'Groups', icon:'groups', class: '',claim:"GetGroupsQuery" },
  { path: '/admin/operationclaim', title: 'OperationClaim', icon:'local_police', class: '', claim:"GetOperationClaimsQuery"},
  { path: '/admin/language', title: 'Languages', icon:'language', class: '', claim:"GetLanguagesQuery" },
  { path: '/admin/translate', title: 'TranslateWords', icon: 'translate', class: '', claim: "GetTranslatesQuery" },
  { path: '/admin/log', title: 'Logs', icon: 'update', class: '', claim: "GetLogDtoQuery" }
];

export const USERROUTES: RouteInfo[] = [ 
  { path: '/admin/doctorType', title: 'DoctorType', icon: 'update', class: '', claim: "GetDoctorTypesQuery" },
  { path: '/admin/currency', title: 'Currency', icon: 'update', class: '', claim: "GetCurrenciesQuery" },
  { path: '/admin/appointmentTime', title: 'AppointmentTime', icon: 'update', class: '', claim: "GetAppointmentTimesQuery" },
  { path: '/admin/patient', title: 'Patient', icon: 'update', class: '', claim: "GetPatientsQuery" },
  { path: '/admin/disease', title: 'Disease', icon: 'update', class: '', claim: "GetDiseasesQuery" },
  { path: '/admin/doctor', title: 'Doctor', icon: 'update', class: '', claim: "GetDoctorsQuery" }
];


@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  adminMenuItems: any[];
  userMenuItems: any[];

  constructor(private router:Router, private authService:AuthService,public translateService:TranslateService) {
    
  }

  ngOnInit() {
  
    this.adminMenuItems = ADMINROUTES.filter(menuItem => menuItem);
    this.userMenuItems = USERROUTES.filter(menuItem => menuItem);

    var lang=localStorage.getItem('lang') || 'tr-TR'
    this.translateService.use(lang);
  }
  isMobileMenu() {
      if ($(window).width() > 991) {
          return false;
      }
      return true;
  };

  checkClaim(claim:string):boolean{
    return this.authService.claimGuard(claim)
  }
  ngOnDestroy() {
    if (!this.authService.loggedIn()) {
      this.authService.logOut();
      this.router.navigateByUrl("/admin/login");
    }
  } 
  menuToggleItemClick(){
    $(".sidebar").css({"display":"none"});
  }

  sidebarLinkClick(url:string){
    if($(window).width() < 600){
      $(".sidebar").css({"display":"none"});  
    }
    this.router.navigate([url]);
  }
  
 }

