import {Component, Input, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojCfg} from "../../../../../moj-cfg";

@Component({
  selector: 'app-detalji',
  templateUrl: './detalji.component.html',
  styleUrls: ['./detalji.component.css']
})
export class DetaljiComponent implements OnInit {

  constructor(private httpClient:HttpClient) { }

  @Input()
  d:any=new Object();
  @Input()
  i:any=new Object();
  donator:any=new Object();
  @Input()
  kategorija:any=new Object();


  ngOnInit(): void {
  }



}
