import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageSaleInvoiceComponent } from './manage-sale-invoice.component';

describe('ManageSaleInvoiceComponent', () => {
  let component: ManageSaleInvoiceComponent;
  let fixture: ComponentFixture<ManageSaleInvoiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageSaleInvoiceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageSaleInvoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
