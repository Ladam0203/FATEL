import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./login/login.component";
import {FatelComponent} from "./fatel/fatel.component";
import {AuthguardService} from "./services/authguard.service";
import {MobileComponent} from "./mobile/mobile.component";
import {DesktopGuard} from "./services/desktop-guard.service";
import {MobileGuard} from "./services/mobile-guard.service";
import {MobileLoginComponent} from "./mobile-login/mobile-login.component";

const routes: Routes = [
  {path: 'login', component: LoginComponent, canActivate: [DesktopGuard]},
  {path: '', component: FatelComponent, canActivate: [AuthguardService]},
  {path: 'mobileLogin', component: MobileLoginComponent, canActivate: [MobileGuard]},
  {path: 'mobile', component: MobileComponent, canActivate: [AuthguardService]},
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
