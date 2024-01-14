import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {FormBuilder} from "@angular/forms";
import {MojCfg} from "../../../../../moj-cfg";

@Component({
  selector: 'app-prikazi',
  templateUrl: './prikazi.component.html',
  styleUrls: ['./prikazi.component.css']
})
export class PrikaziComponent implements OnInit {

  constructor(private httpClient:HttpClient) {}

  @Input()
  d:any=new Object();

  @Input()
  usl:boolean=false;

  @Input()
  itemEdit:any=new Object();

  kolicina=[1,2,3,4,5,6,7,8,9,10];
  kategorije: any = new Object();

  ngOnInit(): void {
    this.getKategorije();

  }

  getKategorije(){
    this.httpClient.get(MojCfg.adresa+"/Kategorija/GetAll")
      .subscribe((x:any)=>{
        this.kategorije=x;
      })
  }
}
