import { GrowlModule } from 'primeng/primeng';
import { AuthenticationService } from './services/authentication.service';
import { CommonModule, DatePipe } from '@angular/common';
import { WorkoutProgramApiService } from './services/workoutprogramapi.service';
import { HttpClient, HttpClientModule, HttpHandler } from '@angular/common/http';
import { AppRouterModule } from '../AppRouterModule/AppRouterModule.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { DataTableModule, DialogModule, SharedModule, ButtonModule, ContextMenuModule, InputTextModule, PasswordModule } from 'primeng/primeng';
import { AppComponent } from './app.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { WorkoutprogramComponent } from './workoutprogramlist/workoutprogram/workoutprogram.component';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { WorkoutProgramListComponent } from './workoutprogramlist/workoutprogramlist.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

import { RegisterValidateService } from './services/register-validate.service'
import { RegisterUserService } from './services/registerUser.service'

@NgModule({
  declarations: [
    AppComponent,
    NotfoundComponent,
    WorkoutprogramComponent,
    WorkoutProgramListComponent,
    LoginComponent,
    RegisterComponent
],
  imports: [
    BrowserModule,
    AppRouterModule,
    DataTableModule,
    SharedModule,
    DialogModule,
    FormsModule,
    BrowserAnimationsModule,
    ButtonModule,
    HttpClientModule,
    ContextMenuModule,
    CommonModule,
    InputTextModule,
    PasswordModule,
    GrowlModule
  ],
  providers: [
    HttpClient,
    WorkoutProgramApiService,
    DatePipe,
    AuthenticationService,
    RegisterValidateService,
    RegisterUserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
 }
