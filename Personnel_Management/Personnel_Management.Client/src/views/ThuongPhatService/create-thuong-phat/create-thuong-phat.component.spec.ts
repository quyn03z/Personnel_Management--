import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateThuongPhatComponent } from './create-thuong-phat.component';

describe('CreateThuongPhatComponent', () => {
  let component: CreateThuongPhatComponent;
  let fixture: ComponentFixture<CreateThuongPhatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateThuongPhatComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateThuongPhatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
