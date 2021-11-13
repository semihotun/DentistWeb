
import { Component } from "@angular/core";
import { NavigationEnd, NavigationStart, Router, RouterEvent } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { filter } from "rxjs/operators";
import { Subject, Subscription } from "rxjs/Rx";
import { AuthService } from "./core/components/admin/login/services/auth.service";

export let browserRefresh = false;
@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
export class AppComponent  {

  subscription: Subscription;
  isRefresh:boolean;
  constructor(
    private translate: TranslateService,
    private authService: AuthService,
    private router: Router
  ) {
    translate.setDefaultLang("tr-TR");
    translate.use("tr-TR");
    // this.router.navigateByUrl("/login");
    // if (!this.authService.loggedIn()) {
    //   this.authService.logOut();
    //   this.router.navigateByUrl("/login");
    // }

    // //Browser refresh edilip edilmediği
    // this.subscription = router.events.subscribe((event) => {
    //   if (event instanceof NavigationStart) {
    //     browserRefresh = !router.navigated;
    //   }
    // });

  }

  ngOnInit():void{
    //Memory Leak
     this.subscription=this.router.events
      .pipe(
        filter(
          (event:RouterEvent):boolean=>{
            return event instanceof NavigationEnd;
          }
        )
      )
      .subscribe((event:NavigationEnd)=>{
        
      });

  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  // isLoggedIn(): boolean {
  //   return this.authService.loggedIn();
  // }

}
