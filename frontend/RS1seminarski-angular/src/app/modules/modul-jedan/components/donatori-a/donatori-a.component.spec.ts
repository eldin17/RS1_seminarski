import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonatoriAComponent } from './donatori-a.component';

describe('DonatoriAComponent', () => {
  let component: DonatoriAComponent;
  let fixture: ComponentFixture<DonatoriAComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DonatoriAComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonatoriAComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
