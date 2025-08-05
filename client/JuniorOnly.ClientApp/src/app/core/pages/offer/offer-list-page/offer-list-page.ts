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
import { BehaviorSubject, combineLatest, map, Observable, switchMap, take } from 'rxjs';

@Component({
  selector: 'app-offer-list-page',
  imports: [OfferList,
    LucideAngularModule,
    FormsModule,
    NgSelectComponent,
    NgSelectModule,
    AsyncPipe,],
  templateUrl: './offer-list-page.html',
  styleUrl: './offer-list-page.scss'
})
export class OfferListPage implements OnInit {
  readonly filterIcon = SlidersHorizontalIcon;
  readonly searchIcon = SearchIcon;
  readonly arrowRightIcon = ArrowRightIcon;
  readonly arrowLeftIcon = ArrowLeftIcon;

  offersResponse$!: Observable<PaginatedResponse<Offer>>;
  paginationData$!: Observable<{
    response: PaginatedResponse<Offer>,
    pages: (string | number)[]
  }>;
  // create a subject, the value can be modified by calling next
  private currentPageSubject = new BehaviorSubject<number>(1);
  // listen when the value when (with next)
  currentPage$ = this.currentPageSubject.asObservable();
  selectedLocation: string = '';
  selectedContract: string = '';

  constructor(private offerService: OfferService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    // add filter directly
    this.route.queryParams.subscribe(params => {
      const page = +params['page'] || 1;
      this.currentPageSubject.next(page);
    });

    // when currentPage change (next on the subject) reexecute to get the new offers
    // and cancel last request if a new one is asked (thanks to switchMap)
    this.paginationData$ = this.currentPage$.pipe(
      switchMap((page) =>
        this.offerService.getOffers(page).pipe(
          map((response) => ({
            response,
            pages: this.getPages(response)
          }))
        )
      )
    );
  }

  getPages(response: PaginatedResponse<Offer>): (string | number)[] {
    const currentPage = response.pageIndex;
    const totalPages = response.totalPages;
    const delta = 2;
    const range = [];
    const rangeWithDots: (string | number)[] = [];

    for (let i = Math.max(2, currentPage - delta);
      i <= Math.min(totalPages - 1, currentPage + delta);
      i++) {
      range.push(i);
    }

    if (currentPage - delta > 2) {
      rangeWithDots.push(1, '...');
    } else {
      rangeWithDots.push(1);
    }

    rangeWithDots.push(...range);

    if (currentPage + delta < totalPages - 1) {
      rangeWithDots.push('...', totalPages);
    } else {
      rangeWithDots.push(totalPages);
    }

    return rangeWithDots;
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

  nextPage() {
    // take(1) listen to only one result than unsubscribe automaticaly
    this.paginationData$.pipe(take(1)).subscribe(data => {
      if (data.response.hasNextPage) {
        const next = this.currentPageSubject.value + 1;
        this.goToPage(next);

      }
    });
  }

  previousPage() {
    this.paginationData$.pipe(take(1)).subscribe(data => {
      if (data.response.hasPreviousPage) {
        const prev = this.currentPageSubject.value - 1;
        this.goToPage(prev);
      }
    });
  }

  goToPage(pageNumber: number) {
    this.scrollToTop();
    this.currentPageSubject.next(pageNumber);

    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { page: pageNumber },
      queryParamsHandling: 'merge',
    });
  }

  private scrollToTop() {
    window.scrollTo({ top: 0, left: 0, behavior: 'smooth' });
  }

}
