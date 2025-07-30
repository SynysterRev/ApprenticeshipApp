import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, MapPin, Clock, Building2, ArrowRight } from 'lucide-angular';
import { Offer } from '../../models/offer.model';
import { ContractFormatPipe } from '../../pipes/contract-format-pipe';
import { SalaryFormatPipe } from '../../pipes/salary-format-pipe';
import { PublishedDateFormatPipe } from '../../pipes/published-date-format-pipe';
import { RemoteFormatPipe } from '../../pipes/remote-format-pipe';

@Component({
  selector: 'app-offer-card-home',
  imports: [LucideAngularModule,
    RouterLink,
    SalaryFormatPipe,
    PublishedDateFormatPipe,
    ContractFormatPipe,
    RemoteFormatPipe],
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
