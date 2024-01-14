import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojCfg} from "../../../../moj-cfg";
import {AuthHelper} from "../../../../helpers/auth-helper";
declare function porukaSuccess(a:string):any;
declare function porukaError(a: string):any;
declare function porukaInfo(a:string):any;
declare function porukaWarning(a: string):any;


@Component({
  selector: 'app-donatori-a',
  templateUrl: './donatori-a.component.html',
  styleUrls: ['./donatori-a.component.css']
})
export class DonatoriAComponent implements OnInit {

  constructor(private httpClient:HttpClient) { }

  donatorDetalji:any=new Object();
  donatori:any;
  osoba:any=new Object();
  velicineOdabir =[6,9,12];
  velicinaStranice: any=6;
  trenutnaStranica: any=1;
  ukupnoArtikala: number=0;
  filter:string = '';
  mail:any=new Object();

  osoba2:any=new Object();
  donator2:any=new Object();
  kontakt2:any=new Object();



  ngOnInit(): void {
    this.getAll();
  }


  getAll():void{
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
    this.getAll();
  }

  promjenaVelicineStranice(event: any) {
    this.velicinaStranice=event.target.value;
    this.trenutnaStranica=1;
    this.getAll();
  }

  delete(x:any){
    let idKontakt=x.donator.kontakt.id;
    let idDonator=x.donator.id;
    let idOsoba=x.id;
    let idKorisnicki=x.donator.korisnickiNalogId;

    this.httpClient.put(MojCfg.adresa+"/Kontakt/SoftDeleteKontakt/"+idKontakt,null)
      .subscribe((x:any)=>{
      });

    this.httpClient.put(MojCfg.adresa+"/Donator/SoftDeleteDonator/"+idDonator,null)
      .subscribe((x:any)=>{
      });

    this.httpClient.put(MojCfg.adresa+"/Osoba/SoftDeleteOsoba/"+idOsoba,null)
      .subscribe((x:any)=>{
      });

    this.httpClient.put(MojCfg.adresa+"/KorisnickiNalog/SoftDeleteNalog/"+idKorisnicki,null)
      .subscribe((x:any)=>{
        porukaSuccess("Uspjesno brisanje!");
      });
    setTimeout(()=>window.location.reload(),500);
  }

  uzmiMail(x:string){
    this.mail.adresaFrom='hubert.grimes@ethereal.email';
    this.mail.sifra='bVmAzFH6t7PKGKUg4J';
    this.mail.adresaTo=x;
  }


  uzmiInfo(d: any) {
    this.osoba2=d;
    this.donator2=d.donator;
    this.kontakt2=d.donator.kontakt;
  }
}
