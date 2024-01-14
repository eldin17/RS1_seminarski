import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {LoginInfo, LoginResponse} from "../../helpers/login-info";
import {MojCfg} from "../../moj-cfg";
import {AuthHelper} from "../../helpers/auth-helper";
import {Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";


declare function porukaSuccess(a:string):any;
declare function porukaError(a: string):any;
declare function porukaInfo(a:string):any;
declare function porukaWarning(a: string):any;


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private httpClient:HttpClient, private router: Router,private formBuilder:FormBuilder) { }

  loginForm!:FormGroup;
  loginInfo:LoginInfo=new LoginInfo();

  ngOnInit(): void {
    if(AuthHelper.isLoggedIn()){
      this.router.navigate(['app']);
    }
    this.loginForm=this.formBuilder.group({
      username:['',Validators.required],
      password:['',Validators.required]
    })
  }




  login() {
    if(this.loginForm.valid){
      this.httpClient.post(MojCfg.adresa+"/KorisnickiNalog/Login",this.loginInfo)
        .subscribe((x:any)=>{
          porukaSuccess('Uspješan login!')
          AuthHelper.setAuthInfo(x);
          if(x!=null)
            this.router.navigateByUrl("/app/home");
        })
    }else{
      console.log("Login forma nije validna !!!");
      porukaError('Unesite sve podatke!');
    }
    setTimeout(()=>{
      if(this.loginForm.valid&&AuthHelper.getAuthInfo().token=='')
        porukaError('Pogrešno korisnicko ime ili lozinka!');
    },300);
  }
}
