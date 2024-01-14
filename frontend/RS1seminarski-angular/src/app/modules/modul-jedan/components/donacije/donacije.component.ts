import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojCfg} from "../../../../moj-cfg";
import {Observable} from "rxjs";
import {AuthHelper} from "../../../../helpers/auth-helper";
declare function porukaSuccess(a:string):any;
declare function porukaError(a: string):any;
declare function porukaInfo(a:string):any;
declare function porukaWarning(a: string):any;


@Component({
  selector: 'app-donacije',
  templateUrl: './donacije.component.html',
  styleUrls: ['./donacije.component.css']
})
export class DonacijeComponent implements OnInit {

  constructor(private httpClient:HttpClient) { }

  donacije:any=[];
  donacijeMoje:any=[];
  user:any=new Object();
  donacija:any=new Object();
  uslov:boolean=false;
  item:any=new Object();

  velicineOdabir =[8,12,16];
  velicinaStranice: any=8;
  trenutnaStranica: any=1;
  ukupnoArtikala: number=0;

  filter2:string = '';

  kategorija:any=new Object();

  ngOnInit(): void {
    this.getDonacije();
    this.getDonator();
  }

  getKategorija(id:any){
    this.httpClient.get(MojCfg.adresa+"/Kategorija/GetById/"+id)
      .subscribe((x:any)=>{
        this.kategorija=x;
      })
  }

  getDonator(){
    this.httpClient.get(MojCfg.adresa+"/Donator/GetByKorisnickiId/"+AuthHelper.getAuthInfo().idLogiranogKorisnika)
      .subscribe((x:any)=>{
        this.user=x;
      })
  }

  getDonacije(){
    this.httpClient.get(MojCfg.adresa+`/Donacija/GetAllDonacije?naslov=${this.filter2}&items_per_page=${this.velicinaStranice}&page_number=${this.trenutnaStranica}`)
      .subscribe((x:any)=>{
        this.donacije=x.pageData;
        this.ukupnoArtikala=x.totalItemsAllPages;
      })
  }

  promijeniStranicu(event: any) {
    this.trenutnaStranica=event;
    this.getDonacije();
  }

  promjenaVelicineStranice(event: any) {
    this.velicinaStranice=event.target.value;
    this.trenutnaStranica=1;
    this.getDonacije();
  }

  getDonacijeMoje(){
    this.httpClient.get(MojCfg.adresa+"/Donacija/GetAllMoje/"+this.user.id)
      .subscribe((x:any)=>{
        this.donacijeMoje=x;
      })
  }

  obrisi(donacija:any,item:any) {
    this.httpClient.put(MojCfg.adresa+"/Donacija/SoftDeleteDonacija/"+donacija.id,null)
      .subscribe((x:any)=>{

      });
    this.httpClient.put(MojCfg.adresa+"/Item/SoftDeleteItem/"+item.id,null)
      .subscribe((x:any)=>{
        porukaSuccess("Uspjesno brisanje!")
      })
    setTimeout(()=>{
       window.location.reload();
    },350);
  }


}
