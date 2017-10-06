import { UserModel } from '../../models/usermodel';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public user: UserModel;

  constructor() { }

  ngOnInit() {
    this.user = {} as UserModel;
  }

  public onclickLogin() {
    console.log("Userobject: " + JSON.stringify(this.user));
  }

}
