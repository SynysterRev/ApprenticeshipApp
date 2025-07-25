import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Company } from './services/company';
import { OfferCard } from './domains/offer/components/offer-card/offer-card';
import { Header } from './core/layout/header/header';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, OfferCard, Header],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  constructor(private companyService: Company) { }

  ngOnInit(): void {
    // this.companyService.getCompanies().subscribe({
    //   next: (data) => {
    //     console.log('Companies:', data);
    //   },
    //   error: (err) => {
    //     console.error('Erreur lors de l\'appel API:', err);
    //   }
    // });
  }
}
