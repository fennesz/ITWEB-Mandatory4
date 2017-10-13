import { AuthenticationService } from './authentication.service';
import { BaseService } from './base.service';
import { ExerciseLog } from '../../models/exerciselog';
import { ExerciseModel } from '../../models/exercisemodel';
import { ExerciseModelDto } from '../../models/dtos/exercisemodeldto';
import { WorkoutProgramModel } from '../../models/workoutprogrammodel';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/concatMap';
import 'rxjs/add/operator/switchMap';
import { Router } from '@angular/router';
import { WorkoutProgramModelDto } from '../../models/dtos/workoutprogrammodeldto';

// Class to hold authentication headers
class AuthenticationHeader {
    headers: HttpHeaders;
    constructor(authService: AuthenticationService) {
        this.headers = new HttpHeaders().set('Authorization', 'Bearer' + authService.getToken());
    }
}

@Injectable()
export class WorkoutProgramApiService extends BaseService {
    authHeader: AuthenticationHeader;
    authIsActive = false;

    constructor(http: HttpClient, authService: AuthenticationService) {
        super(http);
        if (this.authIsActive) {
            this.authHeader = new AuthenticationHeader(authService);
        }
     }

    public editExerciseInWorkoutProgram(id: string, exercise: ExerciseModel): Observable<ExerciseModel> {
        let dto = this.createExerciseDtoFromModel(exercise);
        return this.http.put<ExerciseModel>(this.baseUrl + '/api/workoutprogram/' + id + '/exercise/' + exercise._id, dto, this.authHeader);
    }

    public postExerciseLog(programId: string): Observable<any> {
        return this.http.post(this.baseUrl + '/api/workoutprogram/' + programId + '/logs', {}, this.authHeader);
    }

    public getExerciseLogs(programId: string): Observable<ExerciseLog[]> {
        return this.http.get<ExerciseLog[]>(this.baseUrl + '/api/workoutprogram/' + programId + '/logs', this.authHeader);
    }

    public getWorkoutProgramList(): Observable<WorkoutProgramModel[]> {
        return this.http.get<WorkoutProgramModel[]>(this.baseUrl + '/api/workoutprogram', this.authHeader);
    }

    public getWorkoutProgram(id: string) {
        return this.http.get<WorkoutProgramModel>(this.baseUrl + '/api/workoutprogram/' + id, this.authHeader);
    }

    public deleteExerciseInWorkoutProgram(id: string, exercise: ExerciseModel): Observable<any> {
        return this.http.delete(this.baseUrl + '/api/workoutprogram/' + id + '/exercise/' + exercise._id, this.authHeader);
    }

    public postExerciseToWorkoutProgram(id: string, exercise: ExerciseModel): Observable<any> {
        return this.http.post(this.baseUrl + '/api/workoutprogram/' + id + '/exercise', exercise, this.authHeader)
        .concatMap((link: any) => {
            const url = 'http://' + link.location;
            let dto = this.createExerciseDtoFromModel(exercise);
            return this.http.put<ExerciseModel>(url, dto);
        });
    }

    public deleteWorkoutProgram(id: string) {
        return this.http.delete(this.baseUrl + '/api/workoutprogram/' + id, this.authHeader);
    }

    public postWorkoutProgram(workoutprogrammodel: WorkoutProgramModel): Observable<any> {
        const work = workoutprogrammodel as WorkoutProgramModelDto;
        return this.http.post(this.baseUrl + '/api/workoutprogram', workoutprogrammodel, this.authHeader)
        .concatMap((link: any) => {
            const url = 'http://' + link.location;
            return this.http.put(url, work);
        });
    }

    public editWorkoutProgram(workoutprogrammodel: WorkoutProgramModel): Observable<any> {
        return this.http.patch(this.baseUrl + '/api/workoutprogram/' + workoutprogrammodel._id, 
        {Name: workoutprogrammodel.Name}, this.authHeader);
    }

    private putObjectAndId(id: string, work: WorkoutProgramModelDto): Observable<any> {
        return this.http.put(this.baseUrl + '/api/workoutprogram/' + id, work, this.authHeader);
    }

    private createExerciseDtoFromModel(exercise: ExerciseModel): ExerciseModelDto {
        return {
            Description: exercise.Description,
            ExerciseName: exercise.ExerciseName,
            RepsOrTime: exercise.RepsOrTime,
            Sets: exercise.Sets
        };
    }
}
