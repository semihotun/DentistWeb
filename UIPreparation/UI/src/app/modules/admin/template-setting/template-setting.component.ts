import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertifyService } from 'app/core/services/alertify.service';
import { TemplateSetting } from 'app/models/templateSetting';
import { HeaderService } from 'app/modules/web/home-layout/header/services/header.service';
import { environment } from 'environments/environment';



@Component({
  selector: 'app-template-setting',
  templateUrl: './template-setting.component.html',
  styleUrls: ['./template-setting.component.css']
})
export class TemplateSettingComponent implements OnInit {

  templateSetting:TemplateSetting=new TemplateSetting();
	templateSettingUpdateForm: FormGroup;
  
  constructor(
    private headerService :HeaderService,
    private alertifyService:AlertifyService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.getTemplateSetting();
    this.createTemplateSettingUpdateForm();

  }

  getTemplateSetting(){
    this.headerService.getTeplateSettings().subscribe(data=>{
      this.templateSettingUpdateForm.patchValue(data);
    });
  }

  save(){
		if (this.templateSettingUpdateForm.valid) {
			this.templateSetting = Object.assign({}, this.templateSettingUpdateForm.value)
      console.log(this.templateSetting);
      this.headerService.updateTemplateSetting(this.templateSetting).subscribe(data=>{  
        this.alertifyService.success(data);
      });
		}
  }


  createTemplateSettingUpdateForm() {
		this.templateSettingUpdateForm = this.formBuilder.group({		
			header : [0],
			footer : [0]
		})
	}

  

}
