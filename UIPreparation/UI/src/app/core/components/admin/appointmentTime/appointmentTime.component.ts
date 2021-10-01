import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { AppointmentTime } from './models/AppointmentTime';
import { AppointmentTimeService } from './services/AppointmentTime.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-appointmentTime',
	templateUrl: './appointmentTime.component.html',
	styleUrls: ['./appointmentTime.component.scss']
})
export class AppointmentTimeComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','hour','minutes', 'update','delete'];

	appointmentTimeList:AppointmentTime[];
	appointmentTime:AppointmentTime=new AppointmentTime();

	appointmentTimeAddForm: FormGroup;


	appointmentTimeId:number;

	constructor(private appointmentTimeService:AppointmentTimeService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getAppointmentTimeList();
    }

	ngOnInit() {
		this.createAppointmentTimeAddForm();
	}


	getAppointmentTimeList() {
		this.appointmentTimeService.getAppointmentTimeList().subscribe(data => {
			this.appointmentTimeList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){
		if (this.appointmentTimeAddForm.valid) {
			this.appointmentTime = Object.assign({}, this.appointmentTimeAddForm.value)

			if (this.appointmentTime.id == 0)
				this.addAppointmentTime();
			else
				this.updateAppointmentTime();
		}

	}

	addAppointmentTime(){

		this.appointmentTimeService.addAppointmentTime(this.appointmentTime).subscribe(data => {
			this.getAppointmentTimeList();
			this.appointmentTime = new AppointmentTime();
			jQuery('#appointmenttime').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.appointmentTimeAddForm);

		})

	}

	updateAppointmentTime(){

		this.appointmentTimeService.updateAppointmentTime(this.appointmentTime).subscribe(data => {

			var index=this.appointmentTimeList.findIndex(x=>x.id==this.appointmentTime.id);
			this.appointmentTimeList[index]=this.appointmentTime;
			this.dataSource = new MatTableDataSource(this.appointmentTimeList);
            this.configDataTable();
			this.appointmentTime = new AppointmentTime();
			jQuery('#appointmenttime').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.appointmentTimeAddForm);

		})

	}

	createAppointmentTimeAddForm() {
		this.appointmentTimeAddForm = this.formBuilder.group({		
			id : [0],
			hour : [0, Validators.required],
			minutes : [0, Validators.required]
		})
	}

	deleteAppointmentTime(appointmentTimeId:number){
		this.appointmentTimeService.deleteAppointmentTime(appointmentTimeId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.appointmentTimeList=this.appointmentTimeList.filter(x=> x.id!=appointmentTimeId);
			this.dataSource = new MatTableDataSource(this.appointmentTimeList);
			this.configDataTable();
		})
	}

	getAppointmentTimeById(appointmentTimeId:number){
		this.clearFormGroup(this.appointmentTimeAddForm);
		this.appointmentTimeService.getAppointmentTimeById(appointmentTimeId).subscribe(data=>{
			this.appointmentTime=data;
			this.appointmentTimeAddForm.patchValue(data);
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
