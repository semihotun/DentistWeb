import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { environment } from 'environments/environment';
import { LookUp } from 'app/core/models/lookUp';
import { DoctorService } from '../../../../../services/doctor.service';
import { Doctor } from '../../../../../models/doctor';
import { Router } from '@angular/router';
import { DoctorTypeService } from 'app/services/doctortype.service';

declare var jQuery: any;

@Component({
	selector: 'app-doctorCreate',
	templateUrl: './doctorCreate.component.html',
	styleUrls: ['./doctorCreate.component.scss']
})
export class DoctorCreateComponent implements AfterViewInit, OnInit {
	doctor: Doctor = new Doctor();
	doctorAddForm: FormGroup;
	doctorTypeLookUp: LookUp[];
	constructor(
		private doctorService: DoctorService,
		private doctorTypeService: DoctorTypeService,
		private alertifyService: AlertifyService,
		private formBuilder: FormBuilder,
		private authService: AuthService,
		private router: Router
	) { }

	file: File;

	ngAfterViewInit(): void {
	}
	ngOnInit() {
		this.getDoctorTypes();
		this.createDoctorAddForm();
	}
	getDoctorTypes() {
		this.doctorTypeService.getDoctorTypeLookup().subscribe(data => {
			this.doctorTypeLookUp = data
		})
	}

	changeFile(files: FileList) {
		this.file = files.item(0);
		this.doctorAddForm.controls["File"].setValue(this.file);
	}

	addDoctor() {
		this.doctor = Object.assign({}, this.doctorAddForm.value);
		this.doctor.active = true;
		this.doctor.deleted = false;
		this.doctor.startDateOfWork = new Date().toISOString();

		this.doctorService.addDoctor(this.doctor).subscribe(data => {
			this.alertifyService.success(data);
			this.router.navigate(["admin/doctor"]);
		})
	}
	createDoctorAddForm() {
		this.doctorAddForm = this.formBuilder.group({
			id: [0],
			name: ["", Validators.required],
			surname: ["", Validators.required],
			adress: ["", Validators.required],
			telephone: [5, Validators.required],
			doctorTypeId: [0, Validators.required],
			startDateOfWork: [null],
			active: [true],
			deleted: [false],
			imagePath: [""],
			File: []
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
	checkClaim(claim: string): boolean {
		return this.authService.claimGuard(claim)
	}

}
