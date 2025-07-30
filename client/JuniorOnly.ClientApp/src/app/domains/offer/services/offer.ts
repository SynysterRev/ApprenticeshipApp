import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Offer, OfferCreate, OfferUpdate } from '../models/offer.model';

@Injectable({
  providedIn: 'root'
})
export class OfferService {
  private offerUrl = `${environment.apiURL}/offers`;
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    // withCredentials: true
  };

  constructor(private http: HttpClient) { }

  getOffers(): Observable<Offer[]> {
    return this.http.get<Offer[]>(this.offerUrl, this.httpOptions);
  }

  getOffer(offerId: number): Observable<Offer> {
    return this.http.get<Offer>(`${this.offerUrl}/${offerId}`, this.httpOptions);
  }

  createOffer(offerCreate: OfferCreate): Observable<Offer> {
    return this.http.post<Offer>(this.offerUrl, offerCreate, this.httpOptions);
  }

  updateOffer(offerId: number, offerUpdate: OfferUpdate): Observable<Offer> {
    return this.http.put<Offer>(`${this.offerUrl}/${offerId}`, offerUpdate, this.httpOptions);
  }

  // deleteOffer(offerId: number): Observable<void> {
  //   return this.http.delete<void>(`${this.offerUrl}/${offerId}`, this.httpOptions);
  // }

  softDeleteOffer(offerId: number): Observable<void> {
    return this.http.delete<void>(`${this.offerUrl}/${offerId}`, this.httpOptions);
  }

  getOffersCount(): Observable<number> {
    return this.http.get<number>(`${this.offerUrl}/count`, this.httpOptions);
  }

  getLatestOffers(count: number = 4): Observable<Offer[]> {
    return this.http.get<Offer[]>(`${this.offerUrl}/latest?count=${count}`, this.httpOptions);
  }
}
