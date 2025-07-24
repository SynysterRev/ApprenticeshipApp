import { Component, OnInit  } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Company } from './services/company';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  constructor(private companyService: Company) {}

  ngOnInit(): void {
    this.companyService.getCompanies().subscribe({
      next: (data) => {
        console.log('Companies:', data);
      },
      error: (err) => {
        console.error('Erreur lors de l\'appel API:', err);
      }
    });
  }
}
