import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {LoginInfo,} from "../../helpers/login-info";
import {MojCfg} from "../../moj-cfg";
import {AuthHelper} from "../../helpers/auth-helper";
import {ObjectUnsubscribedError} from "rxjs";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
declare function porukaSuccess(a:string):any;
declare function porukaError(a: string):any;
declare function porukaInfo(a:string):any;
declare function porukaWarning(a: string):any;

@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.css']
})
export class RegistracijaComponent implements OnInit {

  constructor(private httpClient:HttpClient, private router: Router,private formBuilder:FormBuilder) {
    this.regInfo.uloga="Donator";
  }

  kontaktInfo:any=new Object();
  regInfo:any=new Object();
  donatorInfo:any=new Object();
  idDonatora:any=0;
  slika:any='https://icon-library.com/images/default-profile-icon/default-profile-icon-5.jpg';
  file!:File;

  uloge =["Donator","Volonter","Admin"];

  uslov:boolean=false;
  uslov2:boolean=false;

  regForm1!:FormGroup;
  regForm2!:FormGroup;
  regForm3!:FormGroup;

  ngOnInit(): void {
    this.kontaktInfo.emailAdresa='stanton78@ethereal.email';

    this.regForm1=this.formBuilder.group({
      ime:['',Validators.required],
      prezime:['',Validators.required],
      datumRodjenja:['',Validators.required],
    })

    this.regForm2=this.formBuilder.group({
      korisnicko:['',Validators.required],
      lozinka:['',Validators.required],
      uloga:['',Validators.required],
    })

    this.regForm3=this.formBuilder.group({
      telefon:['',Validators.required],
      mail:['',Validators.required],
    })
  }

  registruj() {
    if(this.regForm1.valid && this.regForm2.valid){

    this.httpClient.post(MojCfg.adresa+"/KorisnickiNalog/Register",this.regInfo)
      .subscribe((x:any)=>{
        this.donatorInfo.korisnickiNalogId=x.id;
      })
    }else{
      console.log("Registracija forma nije validna !!!");
    }
  }

  addOsoba(){
    if(this.regForm2.valid && this.regForm1.valid){

    this.httpClient.post(MojCfg.adresa+"/Osoba/AddOsoba",this.regInfo)
      .subscribe((x:any)=>{
        this.donatorInfo.osobaId=x.id;
        this.uslov=true;
      })
    }else{
      console.log("Registracija forma nije validna !!!");
      porukaError('Unesite sve podatke!');
    }
  }

  addDonator(){
    this.httpClient.post(MojCfg.adresa+"/Donator/AddDonator",this.donatorInfo)
      .subscribe((x:any)=>{
           this.idDonatora=x.id;
           this.kontaktInfo.donatorId=x.id;
      })
  }

  previewSlike(event: any) {
    let reader = new FileReader();
    this.file = event.target.files[0];
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = () => {
      this.slika = reader.result;
    };
  }
  proceedUpload() {
    let formdata=new FormData();
    formdata.append("vmSlika",this.file);
    this.httpClient
      .post(MojCfg.adresa+"/Donator/AddSlikaDonatora/"+this.idDonatora,formdata)
      .subscribe((y:any)=>{
      });
  }

  addKontaktDonatora(){
    if(this.regForm3.valid){
    this.httpClient.post(MojCfg.adresa+"/Kontakt/AddKontakt",this.kontaktInfo)
      .subscribe((x:any)=>{
        this.regInfo=new Object();
        porukaSuccess("Uspjesno kreiran korisnički nalog!");
        this.uslov2=true;
      })
    }else{
      console.log("Registracija forma nije validna !!!");
      porukaError('Unesite sve podatke!');
    }
  }
}
