import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditDonacijaComponent } from './edit-donacija.component';

describe('EditDonacijaComponent', () => {
  let component: EditDonacijaComponent;
  let fixture: ComponentFixture<EditDonacijaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditDonacijaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditDonacijaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
