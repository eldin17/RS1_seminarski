import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AppContentComponent} from "./components/app-content/app-content.component";
import {HomeComponent} from "./components/home/home.component";
import {DonatoriComponent} from "./components/donatori/donatori.component";
import {DonacijeComponent} from "./components/donacije/donacije.component";
import {DonatorDetaljiComponent} from "./components/donatori/donator-detalji/donator-detalji.component";
import {DonatorProvjeraGuard} from "../../guards/donator-provjera.guard";
import {DonatoriAComponent} from "./components/donatori-a/donatori-a.component";
import {AdminProvjeraGuard} from "../../guards/admin-provjera.guard";
import {DonacijeAComponent} from "./components/donacije-a/donacije-a.component";

const routes: Routes = [
  {path:'',component:AppContentComponent, children:[
      {path:'home',component:HomeComponent},
      {path:'donatori',component:DonatoriComponent,canActivate:[DonatorProvjeraGuard]},
      {path:'donatori_admin',component:DonatoriAComponent,canActivate:[AdminProvjeraGuard]},
      {path:'donacije',component:DonacijeComponent,canActivate:[DonatorProvjeraGuard]},
      {path:'donacije_admin',component:DonacijeAComponent,canActivate:[AdminProvjeraGuard]},
      {path:'',redirectTo:'/app/home',pathMatch:'full'},
    ]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ModulJedanRoutingModule { }
