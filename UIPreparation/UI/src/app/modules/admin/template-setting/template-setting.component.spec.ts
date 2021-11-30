import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TemplateSettingComponent } from './template-setting.component';

describe('TemplateSettingComponent', () => {
  let component: TemplateSettingComponent;
  let fixture: ComponentFixture<TemplateSettingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TemplateSettingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TemplateSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
