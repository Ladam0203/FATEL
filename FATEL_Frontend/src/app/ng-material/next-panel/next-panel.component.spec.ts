import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NextPanelComponent } from './next-panel.component';

describe('NextPanelComponent', () => {
  let component: NextPanelComponent;
  let fixture: ComponentFixture<NextPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NextPanelComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NextPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
