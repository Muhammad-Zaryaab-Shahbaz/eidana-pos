import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaleInvoicePrintComponent } from './sale-invoice-print.component';

describe('SaleInvoicePrintComponent', () => {
  let component: SaleInvoicePrintComponent;
  let fixture: ComponentFixture<SaleInvoicePrintComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SaleInvoicePrintComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SaleInvoicePrintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
