import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojCfg} from "../../../../../moj-cfg";
import {AuthHelper} from "../../../../../helpers/auth-helper";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
declare function porukaSuccess(a:string):any;
declare function porukaError(a: string):any;
declare function porukaInfo(a:string):any;
declare function porukaWarning(a: string):any;


@Component({
  selector: 'app-moj-profil',
  templateUrl: './moj-profil.component.html',
  styleUrls: ['./moj-profil.component.css']
})
export class MojProfilComponent implements OnInit {

  constructor(private httpClient:HttpClient,private formBuilder:FormBuilder) { }


  @Input()
  d:any=new Object();
  @Input()
  k:any=new Object();
  @Input()
  o:any=new Object();
  file!:File;
  uslov:boolean=false;

  editProfilForm1!:FormGroup;

  ngOnInit(): void {
    this.editProfilForm1=this.formBuilder.group({
      ime:['',Validators.required],
      prezime:['',Validators.required],
      datumRodjenja:['',Validators.required],
      telefon:['',Validators.required],
      mail:['',Validators.required],
    })
  }

  previewSlike(event: any) {
    let reader = new FileReader();
    this.file = event.target.files[0];
    reader.readAsDataURL(event.target.files[0]);
    reader.onload = () => {
      this.d.slikaDonatora = reader.result;
    };
  }
  proceedUpload() {
    let formdata=new FormData();
    formdata.append("vmSlika",this.file);
    this.httpClient
      .post(MojCfg.adresa+"/Donator/AddSlikaDonatora/"+this.d.id,formdata)
      .subscribe((y:any)=>{
      });
  }

  spremi(){
    if(this.uslov){
      this.proceedUpload();
    }

    this.httpClient.put(MojCfg.adresa+"/Osoba/UpdateOsoba/"+this.o.id,this.o)
      .subscribe((x:any)=>{

      })

    this.httpClient.put(MojCfg.adresa+"/Kontakt/UpdateKontakt/"+this.k.id,this.k)
      .subscribe((x:any)=>{
        porukaSuccess("Uspjesan edit!");
      })
  }

}
