import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewClinicComponent } from './new-clinic.component';

describe('NewClinicComponent', () => {
  let component: NewClinicComponent;
  let fixture: ComponentFixture<NewClinicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewClinicComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NewClinicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
