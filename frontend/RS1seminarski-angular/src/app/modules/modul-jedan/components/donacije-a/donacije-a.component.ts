import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojCfg} from "../../../../moj-cfg";
declare function porukaSuccess(a:string):any;
declare function porukaError(a: string):any;
declare function porukaInfo(a:string):any;
declare function porukaWarning(a: string):any;


@Component({
  selector: 'app-donacije-a',
  templateUrl: './donacije-a.component.html',
  styleUrls: ['./donacije-a.component.css']
})
export class DonacijeAComponent implements OnInit {

  constructor(private httpClient:HttpClient) { }

  donacije:any=[];

  donacija:any=new Object();
  item:any=new Object();

  velicineOdabir =[6,9,12];
  velicinaStranice: any=6;
  trenutnaStranica: any=1;
  ukupnoArtikala: number=0;

  filter2:string = '';
  k: any=new Object();


  ngOnInit(): void {
    this.getDonacije();
  }

  getKategorija(){
    this.httpClient.get(MojCfg.adresa+"/Kategorija/GetById/"+this.item.kategorijaId)
      .subscribe((x:any)=>{
        this.k=x;
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



  getDonacijeFilter() {
    if (this.donacije == null)
      return [];
    return this.donacije.filter((x: any)=> x.naslov.length==0 ||
      (x.naslov).toLowerCase().startsWith(this.filter2.toLowerCase()));
  }
}
