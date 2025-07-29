import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfferCardHome } from './offer-card-home';

describe('OfferCardHome', () => {
  let component: OfferCardHome;
  let fixture: ComponentFixture<OfferCardHome>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OfferCardHome]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfferCardHome);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
