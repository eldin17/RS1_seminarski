import { Component, OnInit } from '@angular/core';
import {AuthHelper} from "../../../../helpers/auth-helper";
import {HttpClient} from "@angular/common/http";
import {MojCfg} from "../../../../moj-cfg";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private httpClient:HttpClient) {
    this.uloga=AuthHelper.getAuthInfo().uloga;
  }
  uloga:any='';
  user:any=new Object();
  kontakt:any=new Object();
  osoba:any=new Object();
  idzaosobu:any;


  ngOnInit(): void {
    if(AuthHelper.getAuthInfo().uloga=="Donator"){
    this.httpClient.get(MojCfg.adresa+"/Donator/GetByKorisnickiId/"+AuthHelper.getAuthInfo().idLogiranogKorisnika)
      .subscribe((x:any)=>{
        this.user=x;
        this.kontakt=x.kontakt;
        this.idzaosobu=x.osobaId;
      })
    }else {
      this.user.slikaDonatora="https://media.istockphoto.com/id/1022986820/vector/modern-gradient-background-square-size.jpg?s=170667a&w=0&k=20&c=ETAQ0lI8Pm_nW0o1pdAm-krOvf08HhTHJJwwENjPj3c=";
    }
  }

  getOsoba(id:any){
    this.httpClient.get(MojCfg.adresa+"/Osoba/GetById/"+id)
      .subscribe((x:any)=>{
        this.osoba=x;
      })
  }


  logout() {
    AuthHelper.setLogout();
  }
}
