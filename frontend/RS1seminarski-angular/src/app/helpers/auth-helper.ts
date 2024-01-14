import {LoginResponse} from "./login-info";

export class AuthHelper{

  static setAuthInfo(x:any){
    if(x==null)
      x=new LoginResponse();
    localStorage.setItem("autentifikacija-info",JSON.stringify(x));
  }

  static setLogout(){
    localStorage.removeItem("autentifikacija-info")
  }

  static isLoggedIn(){
    if(AuthHelper.getAuthInfo()==null||AuthHelper.getAuthInfo().token=='')
      return false;
    return true;
  }

  static getAuthInfo():LoginResponse{
    let x:any = localStorage.getItem("autentifikacija-info");
    if (x==="")
      return new LoginResponse();

    try {
      let loginInfo:LoginResponse = JSON.parse(x);

      if (loginInfo==null)
        return new LoginResponse();

      return loginInfo;
    }
    catch (e)
    {
      return new LoginResponse();
    }
  }

}
