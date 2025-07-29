import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, MapPin, Clock, Building2, ArrowRight } from 'lucide-angular';

@Component({
  selector: 'app-offer-card-home',
  imports: [LucideAngularModule,
    RouterLink],
  templateUrl: './offer-card-home.html',
  styleUrl: './offer-card-home.scss'
})
export class OfferCardHome {
  readonly mapIcon = MapPin;
  readonly clockIcon = Clock;
  readonly companyIcon = Building2;
  readonly arrowRightIcon = ArrowRight;
}
