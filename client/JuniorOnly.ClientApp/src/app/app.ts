import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Company } from './services/company';
import { Header } from './core/layout/header/header';
import { Home } from './core/pages/home/home';
import { Footer } from "./core/layout/footer/footer";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Header, Home, Footer],
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
