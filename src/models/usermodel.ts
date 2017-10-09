import { WorkoutProgramModel } from './workoutprogrammodel';

export interface UserModel{
    _id: string;
    Username: string;
    Password: string;
    Hash: string;
    Salt: string;
}