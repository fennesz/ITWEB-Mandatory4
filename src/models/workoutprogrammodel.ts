import { ExerciseModel } from './exercisemodel';
import { UserModel } from './usermodel';

export interface WorkoutProgramModel {
    _id: string;
    Name: string;
    User: UserModel;
    ExerciseList: ExerciseModel[];
}
