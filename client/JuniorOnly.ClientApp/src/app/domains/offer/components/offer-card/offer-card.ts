import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, MapPin, Clock, Building2, ArrowRight } from 'lucide-angular';

@Component({
  selector: 'app-offer-card',
  imports: [LucideAngularModule,
    RouterLink
  ],
  templateUrl: './offer-card.html',
  styleUrl: './offer-card.scss'
})
export class OfferCard {
  imageSrc = 'images/no-logo.png';
  readonly mapIcon = MapPin;
  readonly clockIcon = Clock;
  readonly companyIcon = Building2;
  readonly arrowRightIcon = ArrowRight;
}
