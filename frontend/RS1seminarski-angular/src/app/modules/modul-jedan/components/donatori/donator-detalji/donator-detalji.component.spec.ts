import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonatorDetaljiComponent } from './donator-detalji.component';

describe('DonatorDetaljiComponent', () => {
  let component: DonatorDetaljiComponent;
  let fixture: ComponentFixture<DonatorDetaljiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DonatorDetaljiComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonatorDetaljiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
