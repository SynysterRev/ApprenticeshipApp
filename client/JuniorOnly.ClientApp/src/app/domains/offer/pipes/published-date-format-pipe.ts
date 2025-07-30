import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'publishedDateFormat'
})
export class PublishedDateFormatPipe implements PipeTransform {

  transform(date: Date): unknown {
    const convertedDate = new Date(date);
    const dateText = this.getDateText(convertedDate)
    return `• Il y a ${dateText}`;
  }

  private getDateText(date: Date): string {
    const elapsedTime = (Date.now() - date.getTime()) / 1000;
    const totalMinutes = Math.floor(elapsedTime / 60);
    if (totalMinutes < 60) {
      return `${totalMinutes} min`;
    }

    const totalHours = Math.floor(totalMinutes / 60);
    if (totalHours < 24) {
      return `${totalHours} h`;
    }

    const totalDays = Math.floor(totalHours / 24);
    return `${totalDays} ${totalDays === 1 ? 'jour' : 'jours'}`;
  }

}
