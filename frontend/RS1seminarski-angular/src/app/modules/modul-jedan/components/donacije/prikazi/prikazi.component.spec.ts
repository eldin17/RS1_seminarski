import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrikaziComponent } from './prikazi.component';

describe('PrikaziComponent', () => {
  let component: PrikaziComponent;
  let fixture: ComponentFixture<PrikaziComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrikaziComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PrikaziComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
