import { Component, OnInit } from '@angular/core';
import { OfferList } from '../../../../domains/offer/components/offer-list/offer-list';
import { OfferService } from '../../../../domains/offer/services/offer';
import { Offer, ContractType, RemoteType, SearchCriteria } from '../../../../domains/offer/models/offer.model';
import { LucideAngularModule, SlidersHorizontalIcon, SearchIcon, ArrowRightIcon, ArrowLeftIcon } from 'lucide-angular';
import { NgSelectComponent, NgSelectModule } from '@ng-select/ng-select';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PaginatedResponse } from '../../../../shared/models/paginated-response.model';
import { AsyncPipe } from '@angular/common';
import { BehaviorSubject, combineLatest, map, max, Observable, switchMap, take } from 'rxjs';
import { getContractTypeLabel, getRemoteTypeLabel } from '../../../../domains/offer/offer.utils';
import { parseEnum } from '../../../../shared/utils/parse-enum';

@Component({
  selector: 'app-offer-list-page',
  imports: [OfferList,
    LucideAngularModule,
    FormsModule,
    NgSelectComponent,
    NgSelectModule,
    AsyncPipe,
    ReactiveFormsModule],
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

  defaultCriteria: SearchCriteria = {
    searchTerm: '',
    pageNumber: 1
  };

  // create a subject, the value can be modified by calling next
  private currentPageSubject = new BehaviorSubject<number>(1);
  private filterCriteriaSubject = new BehaviorSubject<SearchCriteria>(this.defaultCriteria);
  // listen when the value when (with next)
  currentPage$ = this.currentPageSubject.asObservable();

  filterControl = new FormGroup({
    searchTerm: new FormControl(''),
    location: new FormControl(''),
    contractType: new FormControl('no-preference'),
    remoteType: new FormControl('no-preference'),
    salary: new FormGroup({
      min: new FormControl(''),
      max: new FormControl(''),
    })
  });

  constructor(private offerService: OfferService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    // add filter directly
    this.route.queryParams.subscribe(params => {
      const page = +params['page'] || 1;

      const criteria: SearchCriteria = {
        searchTerm: params['searchTerm'] || '',
        location: params['location'] || '',
        contractType: parseEnum<ContractType>(params['contractType'], ContractType),
        remoteType: parseEnum<RemoteType>(params['remoteType'], RemoteType),
        minSalary: params['minSalary'] ? +params['minSalary'] : undefined,
        maxSalary: params['maxSalary'] ? +params['maxSalary'] : undefined,
        pageNumber: page
      };

      this.currentPageSubject.next(page);
      this.filterCriteriaSubject.next(this.removeEmptyFields(criteria));

      const queryParams = {
        searchTerm: criteria.searchTerm || null,
        location: criteria.location || null,
        contractType: criteria.contractType?.toString() || null,
        remoteType: criteria.remoteType?.toString() || null,
        salary: {
          min: criteria.minSalary?.toString() || null,
          max: criteria.maxSalary?.toString() || null
        },
      };
      this.filterControl.patchValue(queryParams);
    });

    // when currentPage change (next on the subject) reexecute to get the new offers
    // and cancel last request if a new one is asked (thanks to switchMap)
    this.paginationData$ = combineLatest([
      this.currentPage$,
      this.filterCriteriaSubject.asObservable()
    ]).pipe(
      switchMap(([page, filters]) =>
        this.offerService.search({ ...filters, pageNumber: page }).pipe(
          map(response => ({
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

    if (totalPages <= 1) return [1];

    const pages = new Set<string | number>();

    pages.add(1);

    for (let i = Math.max(1, currentPage - delta);
      i <= Math.min(totalPages, currentPage + delta);
      i++) {
      pages.add(i);
    }

    if (totalPages > 1) {
      pages.add(totalPages);
    }

    const sortedPages = Array.from(pages).sort((a, b) => Number(a) - Number(b));
    const result: (string | number)[] = [];

    for (let i = 0; i < sortedPages.length; i++) {
      if (i > 0 && Number(sortedPages[i]) - Number(sortedPages[i - 1]) > 1) {
        result.push('...');
      }
      result.push(sortedPages[i]);
    }

    return result;
  }

  updateQueryParams() {
    const params: any = {};
    // add query param filter here
    this.route.queryParams.subscribe(params => {
      const page = +params['page'] || 1;
      this.currentPageSubject.next(page);
    });
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

  getRemoteTypes(): { key: RemoteType, label: string }[] {
    return Object.values(RemoteType)
      .filter(value => typeof value === 'number')
      .map(value => ({
        key: value as RemoteType,
        label: getRemoteTypeLabel(value as RemoteType)
      }));
  }

  getAllRemoteOptions(): { key: RemoteType | string, label: string }[] {
    return [
      { key: 'no-preference', label: 'No preference' },
      ...this.getRemoteTypes()
    ];
  }

  getContractTypes(): { key: ContractType, label: string }[] {
    return Object.values(ContractType)
      .filter(value => typeof value === 'number')
      .map(value => ({
        key: value as ContractType,
        label: getContractTypeLabel(value as ContractType)
      }));
  }

  getAllContractOptions(): { key: ContractType | string, label: string }[] {
    const result = [
      { key: 'no-preference', label: 'No preference' },
      ...this.getContractTypes()
    ];
    return result;
  }

  onApplyFilters() {
    const formValue = this.filterControl.value;
    const contractType = parseEnum<ContractType>(formValue.contractType, ContractType);
    const remoteType = parseEnum<RemoteType>(formValue.remoteType, RemoteType);

    const criteria: SearchCriteria = {
      searchTerm: formValue.searchTerm || '',
      location: formValue.location || '',
      contractType: contractType,
      remoteType: remoteType,
      minSalary: formValue.salary?.min ? +formValue.salary.min : undefined,
      maxSalary: formValue.salary?.max ? +formValue.salary.max : undefined,
      pageNumber: this.currentPageSubject.value
    };

    const cleanedCriteria = this.removeEmptyFields(criteria);
    this.filterCriteriaSubject.next(cleanedCriteria);
    this.currentPageSubject.next(1);
    this.filterControl.patchValue({
      ...formValue,
      contractType: formValue.contractType,
      remoteType: formValue.remoteType
    });
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: { ...criteria },
      queryParamsHandling: 'merge',
    });
  }

  onClearFilters() {
    this.filterControl.reset();
    this.filterCriteriaSubject.next(this.defaultCriteria);
    this.currentPageSubject.next(1);
  }

  removeEmptyFields<T>(obj: T): Partial<T> {
    return Object.fromEntries(
      Object.entries(obj as any).filter(([_, value]) =>
        value !== undefined &&
        value !== null &&
        value !== ''
      )
    ) as Partial<T>;
  }

}
