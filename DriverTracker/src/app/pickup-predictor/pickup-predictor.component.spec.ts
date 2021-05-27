import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PickupPredictorComponent } from './pickup-predictor.component';

describe('PickupPredictorComponent', () => {
  let component: PickupPredictorComponent;
  let fixture: ComponentFixture<PickupPredictorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ PickupPredictorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PickupPredictorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
