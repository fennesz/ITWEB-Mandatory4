import { Message } from 'primeng/components/common/api';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { UserModel } from '../../models/usermodel';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public user: UserModel;
  msgs: Message[] = [];

  constructor(private authService: AuthenticationService, private router: Router) { }

  ngOnInit() {
    this.user = {} as UserModel;
  }

  public onclickLogin() {
    console.log("Userobject: " + JSON.stringify(this.user));
    this.authService.LoginUser(this.user).subscribe(res => {
      if (res) {
        this.router.navigate(['/workoutprogram']);
      } else {
        this.msgs = [];
        this.msgs.push({ severity: 'error', summary: 'Error', detail: 'Wrong username or password' });
      }
      return res;
    }, errorCallBack => {
      this.msgs = [];
      this.msgs.push({ severity: 'error', summary: 'Error', detail: 'Couldn\'t access server' });
    }
    );
  }

}
