import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { environment } from 'environments/environment';
import { LookUp } from 'app/core/models/lookUp';
import { DoctorService } from '../../services/doctor.service';
import { Doctor } from '../../models/doctor';
import { ActivatedRoute } from '@angular/router';


declare var jQuery: any;

@Component({
	selector: 'app-doctorUpdate',
	templateUrl: './doctorUpdate.component.html',
	styleUrls: ['./doctorUpdate.component.scss']
})
export class DoctorUpdateComponent implements AfterViewInit, OnInit {
	doctor:Doctor=new Doctor();
	doctorUpdateForm: FormGroup;
	doctorTypeLookUp:LookUp[];
	doctorId:number;

	constructor(private doctorService:DoctorService,
		 private lookupService:LookUpService,
		 private alertifyService:AlertifyService,
		 private formBuilder: FormBuilder,
		  private authService:AuthService,
		  private activatedRoute:ActivatedRoute
		) { }

    ngAfterViewInit(): void {

    }
	ngOnInit() {
		this.updateDoctorUpdateForm();
		this.activatedRoute.params.subscribe(param=>{
			this.doctorId=param["id"];
		})
		this.getDoctorTypes();
		this.getDoctorById();
	}
	getDoctorTypes(){
		this.lookupService.getDoctorTypeLookup().subscribe(data=>{
				this.doctorTypeLookUp=data
		})
	}
	updateDoctor(){
		this.doctor={...this.doctorUpdateForm.value};
		this.doctorService.updateDoctor(this.doctor).subscribe(data => {
			this.alertifyService.success(data);
		})
	}

	updateDoctorUpdateForm() {
		this.doctorUpdateForm = this.formBuilder.group({		
			id : [0],
			name : ["", Validators.required],
			surname : ["", Validators.required],
			adress : ["", Validators.required],
			telephone : [5, Validators.required],
			doctorTypeId : [0, Validators.required],
			startDateOfWork : [null],
			active : [true],
			deleted : [false]
		})
	}
	getDoctorById(){
		this.doctorService.getDoctorById(this.doctorId).subscribe(data=>{
			console.log(data);
			this.doctor=data;
			this.doctorUpdateForm.patchValue(data);
		})
	}
	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}


}
