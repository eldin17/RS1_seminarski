import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojCfg} from "../../../../moj-cfg";
import {Observable} from "rxjs";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private httpClient:HttpClient) { }
  odgovor:any;

  ngOnInit(): void {
  }

  funkcijaAdmin(){
    this.httpClient.get(MojCfg.adresa+"/KorisnickiNalog/TestPristupAdmin",{responseType:'text'})
      .subscribe((x:any)=>{
        this.odgovor=x;
      });
  }

  funkcijaDonator(){
    this.httpClient.get(MojCfg.adresa+"/KorisnickiNalog/TestPristupDonator",{responseType:'text'})
      .subscribe((x:any)=>{
        this.odgovor=x;
      });
  }

}
