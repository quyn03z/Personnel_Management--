import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewEmployeeDetailComponent } from './view-employee-detail.component';

describe('ViewEmployeeDetailComponent', () => {
  let component: ViewEmployeeDetailComponent;
  let fixture: ComponentFixture<ViewEmployeeDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewEmployeeDetailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewEmployeeDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
