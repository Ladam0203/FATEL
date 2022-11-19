import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecordMovementComponent } from './record-movement.component';

describe('RecordMovementComponent', () => {
  let component: RecordMovementComponent;
  let fixture: ComponentFixture<RecordMovementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecordMovementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecordMovementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
