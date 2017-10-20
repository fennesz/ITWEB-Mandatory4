import { Observable } from 'rxjs/Rx';
import { AuthResponse } from '../../models/authresponse';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { UserModel } from '../../models/usermodel';
import { Injectable } from '@angular/core';
import base64url from "base64url";

@Injectable()
export class AuthenticationService extends BaseService {
    constructor(http: HttpClient) {
        super(http);
    }

    public logout(){
        window.localStorage.removeItem(this.workoutProgramTokenKey);
    }

    public setToken(token: string) {
        window.localStorage.setItem(this.workoutProgramTokenKey, token);
    }

    public getToken(): string {
        return window.localStorage.getItem(this.workoutProgramTokenKey);
    }

    // Login user, i virkeligheden
    public LoginUser(user: UserModel): Observable<Boolean> {
        const url = `${this.baseUrl}/api/auth/token`;
        return this.http.post<AuthResponse>(url, user).map(data => {
            this.setToken(data.Token);
            return true;
        }, error => {
            if (error.error instanceof Error) {// A client-side or network error occurred
                console.log('A client-side error occurred:', error.error.message);
                return false;
            }
            console.log(`Backend returned code ${error.status}, body was: ${error.error}`);
            return false;
        });
    }

    public isLoggedIn(): Boolean {
        const token = this.getToken();
        if (token) {
            const payload = JSON.parse(base64url.decode(token.split('.')[1]));
            return payload.exp > Date.now() / 1000;
        } else {
            return false;
        }
    }

    public getCurrentUser(): UserModel {
        if (this.isLoggedIn()) {
            const token = this.getToken();
            const payload = JSON.parse(base64url.decode(token.split('.')[1]));
            const user = {} as UserModel;
            console.log(payload);
            user.Username = payload.Username;
            user.Email = payload.Email;
            return user;
        } else {
            return;
        }
    }
}
