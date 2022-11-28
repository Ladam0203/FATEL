import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NoWarehouseComponent } from './no-warehouse.component';

describe('NoWarehouseComponent', () => {
  let component: NoWarehouseComponent;
  let fixture: ComponentFixture<NoWarehouseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NoWarehouseComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NoWarehouseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
