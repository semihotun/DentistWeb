import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Patient } from './models/Patient';
import { PatientService } from './services/Patient.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-patient',
	templateUrl: './patient.component.html',
	styleUrls: ['./patient.component.scss']
})
export class PatientComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','identificationNumber','name','surname','adress','telephone', 'update','delete'];

	patientList:Patient[];
	patient:Patient=new Patient();

	patientAddForm: FormGroup;


	patientId:number;

	constructor(private patientService:PatientService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getPatientList();
    }

	ngOnInit() {

		this.createPatientAddForm();
	}


	getPatientList() {
		this.patientService.getPatientList().subscribe(data => {
			this.patientList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.patientAddForm.valid) {
			this.patient = Object.assign({}, this.patientAddForm.value)

			if (this.patient.id == 0)
				this.addPatient();
			else
				this.updatePatient();
		}

	}

	addPatient(){

		this.patientService.addPatient(this.patient).subscribe(data => {
			this.getPatientList();
			this.patient = new Patient();
			jQuery('#patient').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.patientAddForm);

		})

	}

	updatePatient(){

		this.patientService.updatePatient(this.patient).subscribe(data => {

			var index=this.patientList.findIndex(x=>x.id==this.patient.id);
			this.patientList[index]=this.patient;
			this.dataSource = new MatTableDataSource(this.patientList);
            this.configDataTable();
			this.patient = new Patient();
			jQuery('#patient').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.patientAddForm);

		})

	}

	createPatientAddForm() {
		this.patientAddForm = this.formBuilder.group({		
			id : [0],
identificationNumber : [0, Validators.required],
name : ["", Validators.required],
surname : ["", Validators.required],
adress : ["", Validators.required],
telephone : [0, Validators.required]
		})
	}

	deletePatient(patientId:number){
		this.patientService.deletePatient(patientId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.patientList=this.patientList.filter(x=> x.id!=patientId);
			this.dataSource = new MatTableDataSource(this.patientList);
			this.configDataTable();
		})
	}

	getPatientById(patientId:number){
		this.clearFormGroup(this.patientAddForm);
		this.patientService.getPatientById(patientId).subscribe(data=>{
			this.patient=data;
			this.patientAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

  }
