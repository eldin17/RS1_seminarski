import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojCfg} from "../../../../moj-cfg";

@Component({
  selector: 'app-donatori',
  templateUrl: './donatori.component.html',
  styleUrls: ['./donatori.component.css']
})
export class DonatoriComponent implements OnInit {

  constructor(private httpClient:HttpClient) { }

  donatorDetalji:any=new Object();
  osoba:any=new Object();
  velicineOdabir =[6,9,12];
  velicinaStranice: any=6;
  trenutnaStranica: any=1;
  ukupnoArtikala: number=0;
  filter:string = '';

  donatori:any;

  ngOnInit(): void {
    this.getDonatori();
  }

  getDonatora(id:any){
    this.httpClient.get(MojCfg.adresa+"/Osoba/GetById/"+id)
      .subscribe((x:any)=>{
        this.osoba=x;
        this.donatorDetalji=x.donator;
      })
  }

  getDonatori():void
  {
    this.httpClient.get(MojCfg.adresa+`/Osoba/GetAllDonatori?ime_prezime=${this.filter}&items_per_page=${this.velicinaStranice}&page_number=${this.trenutnaStranica}`)
      .subscribe((x:any)=>{
        this.donatori=x.pageData;
        this.ukupnoArtikala=x.totalItemsAllPages;
      })
  }

  getDonatoriFilter() {
    if (this.donatori == null)
      return [];
    return this.donatori.filter((x: any)=> x.ime.length==0 ||
      (x.ime + " " + x.prezime).toLowerCase().startsWith(this.filter.toLowerCase()) ||
      (x.prezime + " " + x.ime).toLowerCase().startsWith(this.filter.toLowerCase()));
  }

  promijeniStranicu(event: any) {
    this.trenutnaStranica=event;
    this.getDonatori();
  }

  promjenaVelicineStranice(event: any) {
    this.velicinaStranice=event.target.value;
    this.trenutnaStranica=1;
    this.getDonatori();
  }


}
