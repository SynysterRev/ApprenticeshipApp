import { Pipe, PipeTransform } from '@angular/core';
import { Offer, SalaryPeriod } from '../models/offer.model';

@Pipe({
  name: 'salaryFormat'
})
export class SalaryFormatPipe implements PipeTransform {

  transform(offer: Offer): string {
    const periodText = this.getPeriodText(offer.salaryPeriod);
    return `${offer.salaryMin.toLocaleString()} - ${offer.salaryMax.toLocaleString()} €${periodText}`;
  }

  private getPeriodText(period: SalaryPeriod): string {
    switch (period) {
      case SalaryPeriod.Year: return '/an';
      case SalaryPeriod.Month: return '/mois';
      case SalaryPeriod.Day: return '/jour';
      default: return '';
    }
  }

}
