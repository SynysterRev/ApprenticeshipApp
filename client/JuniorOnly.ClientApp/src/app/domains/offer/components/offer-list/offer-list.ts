import { Component } from '@angular/core';
import { OfferCard } from '../offer-card/offer-card';
import { Offer } from '../../models/offer.model';

@Component({
  selector: 'app-offer-list',
  imports: [OfferCard],
  templateUrl: './offer-list.html',
  styleUrl: './offer-list.scss'
})
export class OfferList {
  offers: Offer[] = [];
}
