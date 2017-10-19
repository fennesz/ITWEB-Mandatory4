import { Observable } from 'rxjs/Rx';
import { AuthResponse } from '../../models/authresponse';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { UserModel } from '../../models/usermodel';
import { Injectable } from '@angular/core';

@Injectable()
export class RegisterUserService extends BaseService {
    constructor(http: HttpClient) {
        super(http);
    }

    RegisterUser(user: UserModel) {
        const url = `${this.baseUrl}/api/auth/register`;
        return this.http.post(url, user).map(data => {
            console.log("SUCCESS");
            // DATA KOMMER TILBAGE

            return true;
        }, error => {
            // UNAUTHORIZED RESPONSE
            if (error.error instanceof Error) {// A client-side or network error occurred
                console.log('A client-side error occurred:', error.error.message);
                return false;
            }
            console.log(`Backend returned code ${error.status}, body was: ${error.error}`);
            return false;
        })
    }
}
