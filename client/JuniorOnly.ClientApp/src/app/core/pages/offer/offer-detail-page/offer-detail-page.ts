import { Component, input, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { LucideAngularModule, ArrowLeftIcon } from 'lucide-angular';
import { Offer } from '../../../../domains/offer/models/offer.model';
import { OfferService } from '../../../../domains/offer/services/offer';
import { switchMap } from 'rxjs';
import { OfferCardDetail } from '../../../../domains/offer/components/offer-card-detail/offer-card-detail';

@Component({
  selector: 'app-offer-detail-page',
  imports: [LucideAngularModule, RouterLink, OfferCardDetail],
  templateUrl: './offer-detail-page.html',
  styleUrl: './offer-detail-page.scss'
})
export class OfferDetailPage implements OnInit {
  offer!: Offer;

  constructor(private offerService: OfferService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.params.pipe(
      switchMap(params => {
        const id = params['id'];
        return this.offerService.getOffer(id);
      })
    ).subscribe({
      next: (offer: Offer) => {
        this.offer = offer;
      },
      error: (error: any) => {
        console.log(error);
      }
    });
  }

  readonly leftArrow = ArrowLeftIcon;

}
