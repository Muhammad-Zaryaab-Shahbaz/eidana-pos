import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersAdditionFieldsComponent } from './users-addition-fields.component';

describe('UsersAdditionFieldsComponent', () => {
  let component: UsersAdditionFieldsComponent;
  let fixture: ComponentFixture<UsersAdditionFieldsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UsersAdditionFieldsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UsersAdditionFieldsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
