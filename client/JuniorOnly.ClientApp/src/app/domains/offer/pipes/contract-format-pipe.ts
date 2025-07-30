import { Pipe, PipeTransform } from '@angular/core';
import { ContractType } from '../models/offer.model';
import { getContractTypeLabel } from '../offer.utils';

@Pipe({
  name: 'contractFormat'
})
export class ContractFormatPipe implements PipeTransform {

  transform(contract: ContractType): string {
    return getContractTypeLabel(contract);
  }

}
