import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfferCardDetail } from './offer-card-detail';

describe('OfferCardDetail', () => {
  let component: OfferCardDetail;
  let fixture: ComponentFixture<OfferCardDetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OfferCardDetail]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfferCardDetail);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
