import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojCfg} from "../../../../../moj-cfg";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
declare function porukaSuccess(a:string):any;
declare function porukaError(a: string):any;
declare function porukaInfo(a:string):any;
declare function porukaWarning(a: string):any;
@Component({
  selector: 'app-mail',
  templateUrl: './mail.component.html',
  styleUrls: ['./mail.component.css']
})
export class MailComponent implements OnInit {

  constructor(private httpClient:HttpClient,private formBuilder:FormBuilder) { }

  @Input()
  mail:any=new Object();

  mailForm1!:FormGroup;


  ngOnInit(): void {
    this.mailForm1=this.formBuilder.group({
      to:['',Validators.required],
      subject:['',Validators.required],
      poruka:['',Validators.required],
    })
  }

  posalji(){
    this.httpClient.post(MojCfg.adresa+"/Email/PosaljiMail",this.mail)
      .subscribe((x:any)=>{
        porukaSuccess("Poslali ste mail!");
      })
  }

}
