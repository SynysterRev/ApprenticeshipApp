import { Routes } from '@angular/router';
import { Home } from './core/pages/home/home';
import { OfferListPage } from './core/pages/offer/offer-list-page/offer-list-page';
import { OfferDetailPage } from './core/pages/offer/offer-detail-page/offer-detail-page';

export const routes: Routes = [
    { path: '', component: Home },
    { path: 'offers', component: OfferListPage },
    { path: 'offers/:id', component: OfferDetailPage },
];

