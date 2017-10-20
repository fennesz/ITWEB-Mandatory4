import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from '../../models/usermodel';
import { RegisterUserService } from '../services/registerUser.service';
import { RegisterValidateService } from '../services/register-validate.service';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {
    public user: UserModel;
    constructor(private registerUserService: RegisterUserService, private userValidationService: RegisterValidateService, private router: Router) { }

    ngOnInit() {
        this.user = {} as UserModel;
    }

    onRegisterClick() {
        if (!this.userValidationService.emailValidate(this.user.Email)) {
            // Notify user of invalid email
            return;
        }

        if (this.userValidationService.registerValidate(this.user)) {
            // Notify user of invalid rest info
            return;
        }
        
        // ALT ER GODT
        this.registerUserService.RegisterUser(this.user).subscribe((value) => {
            console.log(value);
            if(value){
                this.router.navigate(['/']);                        // Send user back to base page                
            }
        });   // Register user

        // Did we succeed in creating an new user?
    }
}


