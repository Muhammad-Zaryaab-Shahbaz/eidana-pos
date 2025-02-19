import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ServiceModuleTsComponent } from './service-module.ts.component';

describe('ServiceModuleTsComponent', () => {
  let component: ServiceModuleTsComponent;
  let fixture: ComponentFixture<ServiceModuleTsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ServiceModuleTsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ServiceModuleTsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
