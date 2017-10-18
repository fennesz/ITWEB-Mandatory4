import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from '../../models/usermodel';
import { AuthenticationService } from '../services/authentication.service';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit{
    public user: UserModel;


    constructor(private authService: AuthenticationService, private router: Router) { }

    ngOnInit(){
        this.user = {} as UserModel;
    }

    onRegisterSubmit(req, res){
        if(!req.body.username || !req.body.email || !req.body.password){
            res
                .status(400)
                .json({"message": "All fields required"});
        } else {
        let user = {Username: req.body.username, 
                    Email: req.body.email, 
                    Password: req.body.passowrd} as UserModel;

        this.authService.registerUser(this.user);
        }
    } 
}


