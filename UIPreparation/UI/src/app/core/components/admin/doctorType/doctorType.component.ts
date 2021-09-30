import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { DoctorType } from './models/DoctorType';
import { DoctorTypeService } from './services/DoctorType.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-doctorType',
	templateUrl: './doctorType.component.html',
	styleUrls: ['./doctorType.component.scss']
})
export class DoctorTypeComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','name', 'update','delete'];

	doctorTypeList:DoctorType[];
	doctorType:DoctorType=new DoctorType();

	doctorTypeAddForm: FormGroup;


	doctorTypeId:number;

	constructor(private doctorTypeService:DoctorTypeService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getDoctorTypeList();
    }

	ngOnInit() {

		this.createDoctorTypeAddForm();
	}


	getDoctorTypeList() {
		this.doctorTypeService.getDoctorTypeList().subscribe(data => {
			this.doctorTypeList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.doctorTypeAddForm.valid) {
			this.doctorType = Object.assign({}, this.doctorTypeAddForm.value)

			if (this.doctorType.id == 0)
				this.addDoctorType();
			else
				this.updateDoctorType();
		}

	}

	addDoctorType(){

		this.doctorTypeService.addDoctorType(this.doctorType).subscribe(data => {
			this.getDoctorTypeList();
			this.doctorType = new DoctorType();
			jQuery('#doctortype').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.doctorTypeAddForm);

		})

	}

	updateDoctorType(){

		this.doctorTypeService.updateDoctorType(this.doctorType).subscribe(data => {

			var index=this.doctorTypeList.findIndex(x=>x.id==this.doctorType.id);
			this.doctorTypeList[index]=this.doctorType;
			this.dataSource = new MatTableDataSource(this.doctorTypeList);
            this.configDataTable();
			this.doctorType = new DoctorType();
			jQuery('#doctortype').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.doctorTypeAddForm);

		})

	}

	createDoctorTypeAddForm() {
		this.doctorTypeAddForm = this.formBuilder.group({		
			id : [0],
name : ["", Validators.required]
		})
	}

	deleteDoctorType(doctorTypeId:number){
		this.doctorTypeService.deleteDoctorType(doctorTypeId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.doctorTypeList=this.doctorTypeList.filter(x=> x.id!=doctorTypeId);
			this.dataSource = new MatTableDataSource(this.doctorTypeList);
			this.configDataTable();
		})
	}

	getDoctorTypeById(doctorTypeId:number){
		this.clearFormGroup(this.doctorTypeAddForm);
		this.doctorTypeService.getDoctorTypeById(doctorTypeId).subscribe(data=>{
			this.doctorType=data;
			this.doctorTypeAddForm.patchValue(data);
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
