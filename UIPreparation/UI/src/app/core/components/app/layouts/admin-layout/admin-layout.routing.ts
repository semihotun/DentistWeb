import { Routes } from '@angular/router';
import { AppointmentTimeComponent } from 'app/core/components/admin/appointmentTime/appointmentTime.component';
import { CurrencyComponent } from 'app/core/components/admin/currency/currency.component';
import { DiseaseComponent } from 'app/core/components/admin/disease/disease.component';
import { DoctorTypeComponent } from 'app/core/components/admin/doctorType/doctorType.component';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { LanguageComponent } from 'app/core/components/admin/language/language.component';
import { LogDtoComponent } from 'app/core/components/admin/log/logDto.component';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { OperationClaimComponent } from 'app/core/components/admin/operationclaim/operationClaim.component';
import { PatientComponent } from 'app/core/components/admin/patient/patient.component';
import { TranslateComponent } from 'app/core/components/admin/translate/translate.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { LoginGuard } from 'app/core/guards/login-guard';
import { DashboardComponent } from '../../dashboard/dashboard.component';





export const AdminLayoutRoutes: Routes = [

    { path: 'dashboard',      component: DashboardComponent,canActivate:[LoginGuard] }, 
    { path: 'user',           component: UserComponent, canActivate:[LoginGuard] },
    { path: 'group',          component: GroupComponent, canActivate:[LoginGuard] },
    { path: 'login',          component: LoginComponent },
    { path: 'language',       component: LanguageComponent,canActivate:[LoginGuard]},
    { path: 'translate',      component: TranslateComponent,canActivate:[LoginGuard]},
    { path: 'operationclaim', component: OperationClaimComponent,canActivate:[LoginGuard]},
    { path: 'log',            component: LogDtoComponent,canActivate:[LoginGuard]},
    { path: 'doctorType',     component: DoctorTypeComponent,canActivate:[LoginGuard]},
    { path: 'appointmentTime',     component: AppointmentTimeComponent,canActivate:[LoginGuard]},
    { path: 'currency',     component: CurrencyComponent,canActivate:[LoginGuard]},
    { path: 'patient',     component: PatientComponent,canActivate:[LoginGuard]},
    { path: 'disease',     component: DiseaseComponent,canActivate:[LoginGuard]},
    
];
