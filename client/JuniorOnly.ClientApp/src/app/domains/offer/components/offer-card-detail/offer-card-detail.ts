import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, MapPin, Clock, Building2 } from 'lucide-angular';
import { Offer } from '../../models/offer.model';
import { ContractFormatPipe } from '../../pipes/contract-format-pipe';
import { SalaryFormatPipe } from '../../pipes/salary-format-pipe';
import { PublishedDateFormatPipe } from '../../pipes/published-date-format-pipe';
import { RemoteFormatPipe } from '../../pipes/remote-format-pipe';

@Component({
  selector: 'app-offer-card-detail',
  imports: [LucideAngularModule,
    RouterLink,
    ContractFormatPipe,
    SalaryFormatPipe,
    PublishedDateFormatPipe,
    RemoteFormatPipe],
  templateUrl: './offer-card-detail.html',
  styleUrl: './offer-card-detail.scss'
})
export class OfferCardDetail {
  imageSrc = 'images/no-logo.png';
  readonly mapIcon = MapPin;
  readonly clockIcon = Clock;
  readonly companyIcon = Building2;

  offer = input.required<Offer>();
}
