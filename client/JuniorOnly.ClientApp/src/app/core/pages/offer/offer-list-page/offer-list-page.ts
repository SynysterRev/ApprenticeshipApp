import { Component, OnInit } from '@angular/core';
import { OfferList } from '../../../../domains/offer/components/offer-list/offer-list';
import { OfferService } from '../../../../domains/offer/services/offer';
import { Offer } from '../../../../domains/offer/models/offer.model';
import { LucideAngularModule, SlidersHorizontalIcon, SearchIcon } from 'lucide-angular';
import { NgSelectComponent, NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-offer-list-page',
  imports: [OfferList,
    LucideAngularModule,
    FormsModule,
    NgSelectComponent,
    NgSelectModule,],
  templateUrl: './offer-list-page.html',
  styleUrl: './offer-list-page.scss'
})
export class OfferListPage implements OnInit {
  readonly filterIcon = SlidersHorizontalIcon;
  readonly searchIcon = SearchIcon;
  offers: Offer[] = [];
  selectedLocation: string = '';
  selectedContract: string = '';

  constructor(private offerService: OfferService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    // add filter directly
    //   this.route.queryParams.subscribe(params => {
    // });
    this.offerService.getOffers().subscribe({
      next: ((offers: Offer[]) => {
        this.offers = offers;
      }),
      error: ((error: any) => {
        console.log(error);
      })
    });
  }

  updateQueryParams() {
    const params: any = {};
    // add query param filter here

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: params,
      queryParamsHandling: 'merge'
    });
  }

}
