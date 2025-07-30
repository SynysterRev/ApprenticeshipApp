import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, MapPin, Clock, Building2, ArrowRight } from 'lucide-angular';
import { Offer } from '../../models/offer.model';
import { ContractFormatPipe } from '../../pipes/contract-format-pipe';
import { LocationFormatPipe } from '../../pipes/location-format-pipe';
import { SalaryFormatPipe } from '../../pipes/salary-format-pipe';
import { PublishedDateFormatPipe } from '../../pipes/published-date-format-pipe';
import { RemoteFormatPipe } from '../../pipes/remote-format-pipe';
import { environment } from '../../../../../environments/environment.development';

@Component({
  selector: 'app-offer-card',
  imports: [LucideAngularModule,
    RouterLink,
    ContractFormatPipe,
    SalaryFormatPipe,
    PublishedDateFormatPipe,
    RemoteFormatPipe
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

  offer = input.required<Offer>();
}
