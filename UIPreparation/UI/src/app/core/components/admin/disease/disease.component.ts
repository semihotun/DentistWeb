import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Disease } from './models/Disease';
import { DiseaseService } from './services/Disease.service';
import { environment } from 'environments/environment';
import { Currency } from '../currency/models/Currency';
import { LookUp } from 'app/core/models/lookUp';

declare var jQuery: any;

@Component({
	selector: 'app-disease',
	templateUrl: './disease.component.html',
	styleUrls: ['./disease.component.scss']
})
export class DiseaseComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','name','price','currencyId', 'update','delete'];

	diseaseList:Disease[];
	currencyList:Currency[];
	currencyLookUp: LookUp[];
	disease:Disease=new Disease();

	diseaseAddForm: FormGroup;


	diseaseId:number;

	constructor(private diseaseService:DiseaseService, 
		private lookupService:LookUpService,
		private alertifyService:AlertifyService,
		private formBuilder: FormBuilder,
		private authService:AuthService,
		 ) { }

    ngAfterViewInit(): void {
        this.getDiseaseList();
    }

	ngOnInit() {
		this.getCurrencyLookUp();

		this.createDiseaseAddForm();
	}

	getCurrencyLookUp()
	{
		this.lookupService.getCurrencyLookup().subscribe(data => {
			this.currencyLookUp = data;
		})
	}

	getDiseaseList() {
		this.diseaseService.getDiseaseList().subscribe(data => {
			this.diseaseList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.diseaseAddForm.valid) {
			this.disease = Object.assign({}, this.diseaseAddForm.value)

			if (this.disease.id == 0)
				this.addDisease();
			else
				this.updateDisease();
		}

	}

	addDisease(){

		this.diseaseService.addDisease(this.disease).subscribe(data => {
			this.getDiseaseList();
			this.disease = new Disease();
			jQuery('#disease').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.diseaseAddForm);

		})

	}

	updateDisease(){

		this.diseaseService.updateDisease(this.disease).subscribe(data => {

			var index=this.diseaseList.findIndex(x=>x.id==this.disease.id);
			this.diseaseList[index]=this.disease;
			this.dataSource = new MatTableDataSource(this.diseaseList);
            this.configDataTable();
			this.disease = new Disease();
			jQuery('#disease').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.diseaseAddForm);

		})

	}

	createDiseaseAddForm() {
		this.diseaseAddForm = this.formBuilder.group({		
			id : [0],
			name : ["", Validators.required],
			price : [0, Validators.required],
			currencyId : [0, Validators.required]
		})
	}

	deleteDisease(diseaseId:number){
		this.diseaseService.deleteDisease(diseaseId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.diseaseList=this.diseaseList.filter(x=> x.id!=diseaseId);
			this.dataSource = new MatTableDataSource(this.diseaseList);
			this.configDataTable();
		})
	}

	getDiseaseById(diseaseId:number){
		this.clearFormGroup(this.diseaseAddForm);
		this.diseaseService.getDiseaseById(diseaseId).subscribe(data=>{
			this.disease=data;
			this.diseaseAddForm.patchValue(data);
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
