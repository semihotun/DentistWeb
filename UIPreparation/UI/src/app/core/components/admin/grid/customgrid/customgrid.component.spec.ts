import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomgridComponent } from './customgrid.component';

describe('CustomgridComponent', () => {
  let component: CustomgridComponent;
  let fixture: ComponentFixture<CustomgridComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomgridComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomgridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
