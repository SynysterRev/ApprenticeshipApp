import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { LucideAngularModule, Search, MapPin, ChevronDown, Users, CircleCheckBig, TrendingUp } from 'lucide-angular';
import { FormsModule } from '@angular/forms';
import { NgSelectComponent, NgSelectModule } from '@ng-select/ng-select';
import { CommonModule } from '@angular/common';
import { StatCard } from '../../../shared/components/stat-card/stat-card';
import { OfferCardHome } from '../../../domains/offer/components/offer-card-home/offer-card-home';
import { OfferService } from '../../../domains/offer/services/offer';
import { Offer } from '../../../domains/offer/models/offer.model';

@Component({
  selector: 'app-home',
  imports: [LucideAngularModule,
    FormsModule,
    NgSelectComponent,
    NgSelectModule,
    CommonModule,
    StatCard,
    OfferCardHome
  ],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home implements OnInit {
  readonly searchIcon = Search;
  readonly mapIcon = MapPin;
  readonly chevronDownIcon = ChevronDown;
  readonly usersIcon = Users;
  readonly circleCheckIcon = CircleCheckBig;
  readonly trendUpIcon = TrendingUp;

  totalOffers: number = 0;
  latestOffers: Offer[] = [];

  selectedLocation?: string;
  items = [
    { name: 'Paris' },
    { name: 'Lyon' },
    { name: 'Marseille' },
    { name: 'Remote' }
  ];

  jobSectors = [
    { name: 'Frontend Development', jobCount: 245 },
    { name: 'Marketing', jobCount: 189 },
    { name: 'Data Analysis', jobCount: 156 },
    { name: 'UX/UI Design', jobCount: 134 },
    { name: 'Customer Success', jobCount: 98 },
    { name: 'Sales', jobCount: 87 },
    { name: 'Content Creation', jobCount: 76 },
    { name: 'Project Management', jobCount: 65 }
  ];

  constructor(private offerService: OfferService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.offerService.getOffersCount().subscribe({
      next: (total: number) => {
        this.totalOffers = total;
      },
      error: (error: any) => {
        console.log(error)
      }
    });
    this.offerService.getLatestOffers().subscribe({
      next: (offers: Offer[]) => {
        this.latestOffers = offers;
      },
      error: (error: any) => {
        console.log(error)
      }
    });
  }

  cardClick(title: string) {

  }
}
