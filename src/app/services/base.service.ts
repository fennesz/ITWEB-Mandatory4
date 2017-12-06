import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export abstract class BaseService {
    protected baseUrl: String;
    protected workoutProgramTokenKey = 'workoutProgramApp';
    protected authIsActive = true;

    constructor(protected http: HttpClient) {
        this.baseUrl = window.location.origin;
        console.log(this.baseUrl);
     }

}
