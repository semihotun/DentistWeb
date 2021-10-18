import { CommonModule } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatRippleModule } from "@angular/material/core";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSelectModule } from "@angular/material/select";
import { MatSortModule } from "@angular/material/sort";
import { MatTableModule } from "@angular/material/table";
import { MatTooltipModule } from "@angular/material/tooltip";
import { RouterModule } from "@angular/router";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { TranslateModule, TranslateLoader } from "@ngx-translate/core";
import { SweetAlert2Module } from "@sweetalert2/ngx-sweetalert2";
import { TranslationService } from "app/core/services/translation.service";
import { NgMultiSelectDropDownModule } from "ng-multiselect-dropdown";
import { AdminCustomLayoutRoutes } from "./admin-custom-layout.routing";
import { AppointmentTimeComponent } from "./appointmentTime/appointmentTime.component";
import { CurrencyComponent } from "./currency/currency.component";
import { DiseaseComponent } from "./disease/disease.component";
import { DoctorCreateComponent } from "./doctor/pages/Create/doctorCreate.component";
import { DoctorComponent } from "./doctor/pages/List/doctor.component";
import { DoctorUpdateComponent } from "./doctor/pages/Update/doctorUpdate.component";
import { DoctorTypeComponent } from "./doctorType/doctorType.component";
import { PatientComponent } from "./patient/patient.component";
import { PatientOperationComponent } from "./patientOperation/patientOperation.component";


export const modules = [
    CommonModule,
    RouterModule.forChild(AdminCustomLayoutRoutes),
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatRippleModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTooltipModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatCheckboxModule,
    NgbModule,
    NgMultiSelectDropDownModule,
    SweetAlert2Module,
    TranslateModule.forChild({
        loader: {
            provide: TranslateLoader,
            //useFactory:layoutHttpLoaderFactory,
            useClass: TranslationService,
            deps: [HttpClient]
        }
    })
  ];

  export const declarationComponent=[
    DoctorTypeComponent,
    AppointmentTimeComponent,
    CurrencyComponent,
    PatientComponent,
    DiseaseComponent,
    DoctorComponent,
    DoctorCreateComponent,
    DoctorUpdateComponent,
    PatientOperationComponent,
  ]


@NgModule({
    imports: [modules],
    declarations: [
        declarationComponent  
    ]
})

export class AdminLayoutCustomModule { }
