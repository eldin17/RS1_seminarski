import { Component } from '@angular/core';
import {AuthHelper} from "./helpers/auth-helper";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'RS1seminarski-angular';

  loginInfo() {
    return AuthHelper.getAuthInfo();
  }

  logout2() {
    AuthHelper.setLogout();
  }

  logout() {
    AuthHelper.setAuthInfo(null);
  }
}
