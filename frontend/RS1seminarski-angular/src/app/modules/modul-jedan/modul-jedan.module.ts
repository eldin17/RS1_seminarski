import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ModulJedanRoutingModule } from './modul-jedan-routing.module';
import { AppContentComponent } from './components/app-content/app-content.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { HomeComponent } from './components/home/home.component';
import { DonatoriComponent } from './components/donatori/donatori.component';
import { DonacijeComponent } from './components/donacije/donacije.component';
import {NgxPaginationModule} from "ngx-pagination";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { DonatorDetaljiComponent } from './components/donatori/donator-detalji/donator-detalji.component';
import { MojProfilComponent } from './components/header/moj-profil/moj-profil.component';
import { DonatoriAComponent } from './components/donatori-a/donatori-a.component';
import { MailComponent } from './components/donatori-a/mail/mail.component';
import { EditComponent } from './components/donatori-a/edit/edit.component';
import { AddComponent } from './components/donacije/add/add.component';
import { EditDonacijaComponent } from './components/donacije/edit-donacija/edit-donacija.component';
import { DonacijeAComponent } from './components/donacije-a/donacije-a.component';
import { DetaljiComponent } from './components/donacije-a/detalji/detalji.component';
import {AppModule} from "../../app.module";
import { PrikaziComponent } from './components/donacije/prikazi/prikazi.component';


@NgModule({
  declarations: [
    AppContentComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    DonatoriComponent,
    DonacijeComponent,
    DonatorDetaljiComponent,
    MojProfilComponent,
    DonatoriAComponent,
    MailComponent,
    EditComponent,
    AddComponent,
    EditDonacijaComponent,
    DonacijeAComponent,
    DetaljiComponent,
    PrikaziComponent,


  ],
    imports: [
        CommonModule,
        ModulJedanRoutingModule,
        NgxPaginationModule,
        FormsModule,
        ReactiveFormsModule,

    ]
})
export class ModulJedanModule { }
