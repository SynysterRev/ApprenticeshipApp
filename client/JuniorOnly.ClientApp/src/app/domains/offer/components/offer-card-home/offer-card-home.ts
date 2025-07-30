import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, MapPin, Clock, Building2, ArrowRight } from 'lucide-angular';
import { Offer } from '../../models/offer.model';
import { PublishedDateFormatPipe, SalaryFormatPipe } from '../../../../shared/pipes';
import { ContractFormatPipe } from '../../pipes/contract-format-pipe';
import { LocationFormatPipe } from '../../pipes/location-format-pipe';

@Component({
  selector: 'app-offer-card-home',
  imports: [LucideAngularModule,
    RouterLink,
    SalaryFormatPipe,
    PublishedDateFormatPipe,
    ContractFormatPipe,
    LocationFormatPipe],
  templateUrl: './offer-card-home.html',
  styleUrl: './offer-card-home.scss'
})
export class OfferCardHome {
  readonly mapIcon = MapPin;
  readonly clockIcon = Clock;
  readonly companyIcon = Building2;
  readonly arrowRightIcon = ArrowRight;

  offer = input.required<Offer>();
}
