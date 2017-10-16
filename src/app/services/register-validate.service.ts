import { Injectable } from '@angular/core';

@Injectable()
export class RegisterValidateService {

  constructor() { }

  registerValidate(user){
    if(user.username == undefined || user.email == undefined || user.password == undefined){
      return false;
    } else {
    return true;
  }
}

emailValidate(email){
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
  }
}
