import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LucideAngularModule, Briefcase } from 'lucide-angular';

@Component({
  selector: 'app-footer',
  imports: [LucideAngularModule,
    RouterLink
  ],
  templateUrl: './footer.html',
  styleUrl: './footer.scss'
})
export class Footer {
  logo = "";
  readonly briefCaseIcon = Briefcase;
}
