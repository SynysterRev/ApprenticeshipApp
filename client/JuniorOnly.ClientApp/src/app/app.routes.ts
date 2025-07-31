import { Routes } from '@angular/router';
import { Home } from './core/pages/home/home';
import { OfferListPage } from './core/pages/offer/offer-list-page/offer-list-page';

export const routes: Routes = [
    {path: '', component: Home},
    {path: 'offers', component: OfferListPage}
];
