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


declare var jQuery: any;

@Component({
	selector: 'app-doctor',
	templateUrl: './doctor.component.html',
	styleUrls: ['./doctor.component.scss']
})
export class DoctorComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id', 'name', 'surname', 'adress', 'telephone', 'doctorTypeId', 'startDateOfWork', 'active', 'update', 'delete'];
	doctorList: Doctor[];
	doctorTypeLookUp: LookUp[];

	constructor(private doctorService: DoctorService,
		private lookupService: LookUpService,
		private alertifyService: AlertifyService,
		private formBuilder: FormBuilder,
		private authService: AuthService) { }


	deleteDoctor(doctorId: number) {
		this.doctorService.deleteDoctor(doctorId).subscribe(data => {
			this.alertifyService.success(data.toString());
			this.doctorList = this.doctorList.filter(x => x.id != doctorId);
			this.dataSource = new MatTableDataSource(this.doctorList);
			this.configDataTable();
		})
	}
	ngAfterViewInit(): void {
		this.getDoctorList();
	}
	ngOnInit() {
		this.getDoctorTypes();
	}
	getDoctorTypes() {
		this.lookupService.getDoctorTypeLookup().subscribe(data => {
			this.doctorTypeLookUp = data
		})
	}
	getDoctorList() {
		this.doctorService.getDoctorList().subscribe(data => {
			this.doctorList = data;
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		});
	}
	checkClaim(claim: string): boolean {
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
