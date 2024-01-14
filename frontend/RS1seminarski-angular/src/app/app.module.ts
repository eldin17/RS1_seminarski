import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';

import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";

import {RouterModule, Routes} from "@angular/router";
import { LoginComponent } from './components/login/login.component';
import { RegistracijaComponent } from './components/registracija/registracija.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import {LoginProvjeraGuard} from "./guards/login-provjera.guard";
import {AuthInterceptor} from "./services/auth.interceptor";
import { NgxPaginationModule } from 'ngx-pagination';


const routes:Routes=[
  {path:'login',component:LoginComponent},
  {path:'registracija',component:RegistracijaComponent},
  {path:'',redirectTo:'/login',pathMatch:'full'},
  {path:'app',canActivate:[LoginProvjeraGuard],
    loadChildren:()=>
      import('./modules/modul-jedan/modul-jedan.module')
        .then((m)=>m.ModulJedanModule)},
  {path:'**',component:NotFoundComponent},

]

@NgModule({
    declarations: [
        AppComponent,
        LoginComponent,
        RegistracijaComponent,
        NotFoundComponent,



    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        RouterModule.forRoot(routes),
        ReactiveFormsModule,
        NgxPaginationModule
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true,
        }
    ],
    exports: [

    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
