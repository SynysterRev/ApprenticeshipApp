import { Component, OnInit } from '@angular/core';
import { OfferList } from '../../../../domains/offer/components/offer-list/offer-list';
import { OfferService } from '../../../../domains/offer/services/offer';
import { Offer } from '../../../../domains/offer/models/offer.model';
import { LucideAngularModule, SlidersHorizontalIcon, SearchIcon, ArrowRightIcon, ArrowLeftIcon } from 'lucide-angular';
import { NgSelectComponent, NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PaginatedResponse } from '../../../../shared/components/stat-card/models/paginated-response.model';
import { AsyncPipe } from '@angular/common';
import { map, Observable } from 'rxjs';

@Component({
  selector: 'app-offer-list-page',
  imports: [OfferList,
    LucideAngularModule,
    FormsModule,
    NgSelectComponent,
    NgSelectModule,
    AsyncPipe],
  templateUrl: './offer-list-page.html',
  styleUrl: './offer-list-page.scss'
})
export class OfferListPage implements OnInit {
  readonly filterIcon = SlidersHorizontalIcon;
  readonly searchIcon = SearchIcon;
  readonly arrowRightIcon = ArrowRightIcon;
  readonly arrowLeftIcon = ArrowLeftIcon;

  currentPage: number = 1;
  totalPages: number = 0;
  offersResponse$!: Observable<PaginatedResponse<Offer>>;
  paginationData$!: Observable<{
    response: PaginatedResponse<Offer>,
    pages: number[]
  }>;
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
    this.offersResponse$ = this.offerService.getOffers(this.currentPage);

    this.paginationData$ = this.offersResponse$.pipe(
      map(response => ({
        response: response,
        pages: Array.from({ length: response.totalPages }, (_, i) => i + 1)
      }))
    );
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

  // nextPage() {
  //   if (!this.offersResponse$.hasNextPage) {
  //     return;
  //   }
  //   this.currentPage++;
  //   this.offerService.getOffers(this.currentPage).subscribe({
  //     next: ((offers: PaginatedResponse<Offer>) => {
  //       this.offersResponse$ = offers;
  //       this.offers = offers.items;
  //     }),
  //     error: ((error: any) => {
  //       console.log(error);
  //     })
  //   });
  // }

  // previousPage() {
  //   if (!this.offersResponse$.hasPreviousPage) {
  //     return;
  //   }
  //   this.currentPage--;
  //   this.offerService.getOffers(this.currentPage).subscribe({
  //     next: ((offers: PaginatedResponse<Offer>) => {
  //       this.offersResponse$ = offers;
  //       this.offers = offers.items;
  //     }),
  //     error: ((error: any) => {
  //       console.log(error);
  //     })
  //   });
  // }

}
