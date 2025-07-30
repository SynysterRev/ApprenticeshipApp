import { Pipe, PipeTransform } from '@angular/core';
import { Offer } from '../models/offer.model';
import { getRemoteTypeLabel } from '../offer.utils';

@Pipe({
  name: 'locationFormat'
})
export class LocationFormatPipe implements PipeTransform {

  transform(offer: Offer): string {
    if (!offer) {
      return '';
    }
    if (offer.remoteType === 2) {
      return getRemoteTypeLabel(offer.remoteType);
    }
    return offer.location;
  }

}
