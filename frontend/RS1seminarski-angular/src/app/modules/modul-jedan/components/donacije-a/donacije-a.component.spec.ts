import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonacijeAComponent } from './donacije-a.component';

describe('DonacijeAComponent', () => {
  let component: DonacijeAComponent;
  let fixture: ComponentFixture<DonacijeAComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DonacijeAComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonacijeAComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
