import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-stat-card',
  imports: [],
  templateUrl: './stat-card.html',
  styleUrl: './stat-card.scss'
})
export class StatCard {
  title = input.required<string>();
  count = input.required<number>();
  unit = input.required<string>();
  onCardClick = output<string>();

  cardClick()
  {
    this.onCardClick.emit(this.title());
  }
}
