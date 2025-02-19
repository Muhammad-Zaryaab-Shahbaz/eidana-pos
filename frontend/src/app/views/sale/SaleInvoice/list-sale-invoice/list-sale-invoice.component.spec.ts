import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListSaleInvoiceComponent } from './list-sale-invoice.component';

describe('ListSaleInvoiceComponent', () => {
  let component: ListSaleInvoiceComponent;
  let fixture: ComponentFixture<ListSaleInvoiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListSaleInvoiceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListSaleInvoiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
