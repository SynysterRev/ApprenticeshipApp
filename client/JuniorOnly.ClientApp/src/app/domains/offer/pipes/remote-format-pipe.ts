import { Pipe, PipeTransform } from '@angular/core';
import { getRemoteTypeLabel } from '../offer.utils';
import { RemoteType } from '../models/offer.model';

@Pipe({
  name: 'remoteFormat'
})
export class RemoteFormatPipe implements PipeTransform {

  transform(remoteType: RemoteType): string {
    return getRemoteTypeLabel(remoteType);
  }

}
