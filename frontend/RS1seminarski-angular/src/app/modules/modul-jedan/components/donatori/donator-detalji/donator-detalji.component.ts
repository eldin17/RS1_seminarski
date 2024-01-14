import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojCfg} from "../../../../../moj-cfg";
import {AuthHelper} from "../../../../../helpers/auth-helper";

@Component({
  selector: 'app-donator-detalji',
  templateUrl: './donator-detalji.component.html',
  styleUrls: ['./donator-detalji.component.css']
})
export class DonatorDetaljiComponent implements OnInit {

  constructor(private httpClient:HttpClient) { }

  @Input()
  d:any=new Object();
  @Input()
  o:any=new Object();

  niz:any;

  ngOnInit(): void {
    this.niz= new Array(this.d.brojDonacija);
  }



}
