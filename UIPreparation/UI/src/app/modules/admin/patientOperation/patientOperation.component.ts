import { Component, AfterViewInit, OnInit, ViewChild, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { PatientOperation } from '../../../models/patientOperation';
import { PatientOperationService } from '../../../services/patientoperation.service';
import { Disease } from '../../../models/disease';
import { DiseaseService } from 'app/services/disease.service';
import { PatientOperationDto } from 'app/models/dto/PatientOperationDTO';

declare var jQuery: any;

@Component({
	selector: 'app-patientOperation',
	templateUrl: './patientOperation.component.html',
	styleUrls: ['./patientOperation.component.scss']
})
export class PatientOperationComponent implements AfterViewInit, OnInit {
	
	@Input() patientId;
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['diseaseName','delete'];

	patientOperationList:PatientOperationDto[];
	patientOperation:PatientOperation=new PatientOperation();

	patientOperationAddForm: FormGroup;

	diseaseLookUp:Disease[];

	patientOperationId:number;

	constructor(private patientOperationService:PatientOperationService,
		 private diseaseService:DiseaseService,
		 private alertifyService:AlertifyService,
		 private formBuilder: FormBuilder, 
		 private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getPatientOperationList();
    }

	ngOnInit() {
		
		this.getDisease();
		this.createPatientOperationAddForm();
	}


	getPatientOperationList() {
		this.patientOperationService.getPatientOperationDtoByPatientId(this.patientId).subscribe(data => {	
			this.patientOperationList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){
		this.patientOperationAddForm.controls["patientId"].setValue(this.patientId);
		this.patientOperation.patientId = this.patientId;
		if (this.patientOperationAddForm.valid) {
			this.patientOperation = Object.assign({}, this.patientOperationAddForm.value)

			if (this.patientOperation.id == 0)
				this.addPatientOperation();
			else
				this.updatePatientOperation();
		}

	}

	addPatientOperation(){

		this.patientOperationService.addPatientOperation(this.patientOperation).subscribe(data => {
			this.getPatientOperationList();
			this.patientOperation = new PatientOperation();
			jQuery('#patientoperation').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.patientOperationAddForm);

		})

	}

	updatePatientOperation(){

		this.patientOperationService.updatePatientOperation(this.patientOperation).subscribe(data => {

			var index=this.patientOperationList.findIndex(x=>x.id==this.patientOperation.id);
			this.patientOperationList[index]=this.patientOperation;
			this.dataSource = new MatTableDataSource(this.patientOperationList);
            this.configDataTable();
			this.patientOperation = new PatientOperation();
			jQuery('#patientoperation').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.patientOperationAddForm);

		})

	}

	createPatientOperationAddForm() {
		this.patientOperationAddForm = this.formBuilder.group({		
			id : [0],
			patientId : [0, Validators.required],
			diseaseId : [0, Validators.required]
		})
	}

	deletePatientOperation(patientOperationId:number){
		this.patientOperationService.deletePatientOperation(patientOperationId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.patientOperationList=this.patientOperationList.filter(x=> x.id!=patientOperationId);
			this.dataSource = new MatTableDataSource(this.patientOperationList);
			this.configDataTable();
		})
	}

	getPatientOperationById(patientOperationId:number){
		this.clearFormGroup(this.patientOperationAddForm);
		this.patientOperationService.getPatientOperationById(patientOperationId).subscribe(data=>{
			this.patientOperation=data;
			this.patientOperationAddForm.patchValue(data);
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

	getDisease() {
		this.diseaseService.getDiseaseLookUp().subscribe(data => {
			this.diseaseLookUp = data
		})
	}

  }
