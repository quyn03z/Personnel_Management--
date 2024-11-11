import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateThuongPhatComponent } from './update-thuong-phat.component';

describe('UpdateThuongPhatComponent', () => {
  let component: UpdateThuongPhatComponent;
  let fixture: ComponentFixture<UpdateThuongPhatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdateThuongPhatComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UpdateThuongPhatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
