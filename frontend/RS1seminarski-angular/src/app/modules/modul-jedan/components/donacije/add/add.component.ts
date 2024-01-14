import { Component, OnInit } from '@angular/core';
import {MojCfg} from "../../../../../moj-cfg";
import {HttpClient} from "@angular/common/http";
import {AuthHelper} from "../../../../../helpers/auth-helper";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
declare function porukaSuccess(a:string):any;
declare function porukaError(a: string):any;
declare function porukaInfo(a:string):any;
declare function porukaWarning(a: string):any;

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {

  constructor(private httpClient: HttpClient,private formBuilder:FormBuilder) {

  }

  donacija: any = new Object();
  item: any = new Object();
  kategorije: any = new Object();
  kolicina=[1,2,3,4,5,6,7,8,9,10];
  donatorID:any;
  donacijaID:any;
  selectedFiles?: FileList;
  previews:any =[];
  file!: File;
  uslov:boolean =false;

  addForm1!:FormGroup;
  addForm2!:FormGroup;

  ngOnInit(): void {
    this.httpClient.get(MojCfg.adresa+"/Donator/GetByKorisnickiId/"+AuthHelper.getAuthInfo().idLogiranogKorisnika)
      .subscribe((x:any)=>{
        this.donatorID=x.id;
      })
    this.getKategorije();
    this.item.kolicina=1;

    this.addForm1=this.formBuilder.group({
      naslov:['',Validators.required],
      opis:['',Validators.required],
    })
    this.addForm2=this.formBuilder.group({
      naziv:['',Validators.required],
      kolicina:['',Validators.required],
      proizvodjac:['',Validators.required],
      kategorija:['',Validators.required],
    })

  }

  getKategorije(){
    this.httpClient.get(MojCfg.adresa+"/Kategorija/GetAll")
      .subscribe((x:any)=>{
        this.kategorije=x;
      })
  }

  dodajItem(){
    this.item.donacijaId=this.donacijaID;
    this.httpClient.post(MojCfg.adresa+"/Item/AddItem",this.item)
      .subscribe((x: any) => {

      })
  }

  donacijaUpload() {
    this.donacija.donatorId=this.donatorID;
    this.httpClient.post(MojCfg.adresa + "/Donacija/AddDonacija", this.donacija)
      .subscribe((x: any) => {
        this.donacijaID=x.id;
      })
  }


  previewSlike(event: any): void {
    this.selectedFiles = event.target.files;
    this.previews = [];
    if (this.selectedFiles && this.selectedFiles[0]) {
      const count = this.selectedFiles.length;
      for (let i = 0; i < count; i++) {
        const reader = new FileReader();

        reader.onload = (e: any) => {
          this.previews.push(e.target.result);
        }
        reader.readAsDataURL(this.selectedFiles[i]);
      }
    }
  }

  proceedUpload() {
    if (this.selectedFiles && this.selectedFiles[0]) {
      const count = this.selectedFiles.length;
      let formdata = new FormData();
      for (let i = 0; i < count; i++) {
        formdata.append("vmSlike", this.selectedFiles[i]);

      }
      this.httpClient
        .post(MojCfg.adresa + "/Donacija/AddSlikeDonacije/" + this.donacijaID, formdata)
        .subscribe((y: any) => {
        });

    }


  }

  updateBrojevi() {
    this.httpClient.put(MojCfg.adresa+"/Donator/UpdateDonatorBroj/"+this.donatorID,null)
      .subscribe((x: any) => {
        porukaSuccess("Uspjesno ste dodali donaciju!")
      });
    setTimeout(()=>window.location.reload(),250);
  }
}
