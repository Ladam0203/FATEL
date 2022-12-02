import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LoginComponent} from "./login/login.component";
import {FatelComponent} from "./fatel/fatel.component";
import {AuthguardService} from "./services/authguard.service";

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: '', component: FatelComponent, canActivate: [AuthguardService]},
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
