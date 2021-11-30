import { Routes } from "@angular/router";
import { LoginGuard } from "app/core/guards/login-guard";
import { AppointmentTimeComponent } from "./appointmentTime/appointmentTime.component";
import { CurrencyComponent } from "./currency/currency.component";
import { DiseaseComponent } from "./disease/disease.component";
import { DoctorCreateComponent } from "./doctor/pages/Create/doctorCreate.component";
import { DoctorComponent } from "./doctor/pages/List/doctor.component";
import { DoctorUpdateComponent } from "./doctor/pages/Update/doctorUpdate.component";
import { DoctorTypeComponent } from "./doctorType/doctorType.component";
import { PatientComponent } from "./patient/patient.component";
import { PatientOperationComponent } from "./patientOperation/patientOperation.component";
import { TemplateSettingComponent } from "./template-setting/template-setting.component";




export const AdminCustomLayoutRoutes: Routes = [
    { path: 'doctorType',     component: DoctorTypeComponent,canActivate:[LoginGuard]},
    { path: 'appointmentTime',     component: AppointmentTimeComponent,canActivate:[LoginGuard]},
    { path: 'currency',     component: CurrencyComponent,canActivate:[LoginGuard]},
    { path: 'patient',     component: PatientComponent,canActivate:[LoginGuard]},
    { path: 'disease',     component: DiseaseComponent,canActivate:[LoginGuard]},
    { path: 'doctor',     component: DoctorComponent,canActivate:[LoginGuard]},
    { path: 'doctorCreate',     component: DoctorCreateComponent,canActivate:[LoginGuard]},
    { path: 'doctorUpdate/:id',     component: DoctorUpdateComponent,canActivate:[LoginGuard]},
    { path :'patientOperation' ,  component:PatientOperationComponent,canActivate:[LoginGuard]},
    { path :'template-setting' ,  component:TemplateSettingComponent,canActivate:[LoginGuard]}
];
