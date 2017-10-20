import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export abstract class BaseService {
    protected baseUrl: String = 'https://itweb-mandatory2.herokuapp.com';
    protected workoutProgramTokenKey = 'workoutProgramApp';
    protected authIsActive = true;

    constructor(protected http: HttpClient) { }

}
