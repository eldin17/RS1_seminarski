import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AuthHelper} from "../../../../helpers/auth-helper";

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

  constructor() {
    this.uloga=AuthHelper.getAuthInfo().uloga;
  }
  uloga:any='';

  ngOnInit(): void {
  }

}
