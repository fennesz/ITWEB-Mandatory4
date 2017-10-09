import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { UserModel } from '../../models/usermodel';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthenticationService extends BaseService {
    constructor(http: HttpClient) {
        super(http);
     }
     public saveToken(token: string) {
         window.localStorage.setItem(this.workoutProgramTokenKey, token);
     }

     public getToken(): string {
         return window.localStorage.getItem(this.workoutProgramTokenKey);
     }

     public RegisterUser(user: UserModel): boolean {
        const url = `${this.baseUrl}/api/register`;
        this.http.post<AuthResponse>
     }
}