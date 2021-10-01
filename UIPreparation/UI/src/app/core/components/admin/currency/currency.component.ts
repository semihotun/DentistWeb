import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Currency } from './models/Currency';
import { CurrencyService } from './services/Currency.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-currency',
	templateUrl: './currency.component.html',
	styleUrls: ['./currency.component.scss']
})
export class CurrencyComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','abbreviation', 'update','delete'];

	currencyList:Currency[];
	currency:Currency=new Currency();

	currencyAddForm: FormGroup;


	currencyId:number;

	constructor(private currencyService:CurrencyService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getCurrencyList();
    }

	ngOnInit() {

		this.createCurrencyAddForm();
	}


	getCurrencyList() {
		this.currencyService.getCurrencyList().subscribe(data => {
			this.currencyList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.currencyAddForm.valid) {
			this.currency = Object.assign({}, this.currencyAddForm.value)

			if (this.currency.id == 0)
				this.addCurrency();
			else
				this.updateCurrency();
		}

	}

	addCurrency(){

		this.currencyService.addCurrency(this.currency).subscribe(data => {
			this.getCurrencyList();
			this.currency = new Currency();
			jQuery('#currency').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.currencyAddForm);

		})

	}

	updateCurrency(){

		this.currencyService.updateCurrency(this.currency).subscribe(data => {

			var index=this.currencyList.findIndex(x=>x.id==this.currency.id);
			this.currencyList[index]=this.currency;
			this.dataSource = new MatTableDataSource(this.currencyList);
            this.configDataTable();
			this.currency = new Currency();
			jQuery('#currency').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.currencyAddForm);

		})

	}

	createCurrencyAddForm() {
		this.currencyAddForm = this.formBuilder.group({		
			id : [0],
abbreviation : ["", Validators.required]
		})
	}

	deleteCurrency(currencyId:number){
		this.currencyService.deleteCurrency(currencyId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.currencyList=this.currencyList.filter(x=> x.id!=currencyId);
			this.dataSource = new MatTableDataSource(this.currencyList);
			this.configDataTable();
		})
	}

	getCurrencyById(currencyId:number){
		this.clearFormGroup(this.currencyAddForm);
		this.currencyService.getCurrencyById(currencyId).subscribe(data=>{
			this.currency=data;
			this.currencyAddForm.patchValue(data);
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
