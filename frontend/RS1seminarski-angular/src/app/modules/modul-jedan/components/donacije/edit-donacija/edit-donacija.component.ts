import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojCfg} from "../../../../../moj-cfg";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
declare function porukaSuccess(a:string):any;
declare function porukaError(a: string):any;
declare function porukaInfo(a:string):any;
declare function porukaWarning(a: string):any;

@Component({
  selector: 'app-edit-donacija',
  templateUrl: './edit-donacija.component.html',
  styleUrls: ['./edit-donacija.component.css']
})
export class EditDonacijaComponent implements OnInit {

  constructor(private httpClient:HttpClient,private formBuilder:FormBuilder) {}

  @Input()
  d:any=new Object();

  @Input()
  usl:boolean=false;

  @Input()
  itemEdit:any=new Object();

  kolicina=[1,2,3,4,5,6,7,8,9,10];
  kategorije: any = new Object();

  selectedFiles?: FileList;
  previews:any =[];
  file!: File;

  editForm1!:FormGroup;



  ngOnInit(): void {
    this.getKategorije();
    this.editForm1=this.formBuilder.group({
      naslov:['',Validators.required],
      opis:['',Validators.required],
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

  sacuvaj() {
    this.httpClient.put(MojCfg.adresa+"/Donacija/UpdateDonacija/"+this.d.id,this.d)
      .subscribe((x:any)=>{

      });

    this.httpClient.put(MojCfg.adresa+"/Item/UpdateItem/"+this.itemEdit.id,this.itemEdit)
      .subscribe((x:any)=>{
          porukaSuccess("Uspjesan edit!")
      });
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
        .post(MojCfg.adresa + "/Donacija/AddSlikeDonacije/" + this.d.id, formdata)
        .subscribe((y: any) => {
        });
      setTimeout(()=>window.location.reload(),1000);
    }


  }

}
