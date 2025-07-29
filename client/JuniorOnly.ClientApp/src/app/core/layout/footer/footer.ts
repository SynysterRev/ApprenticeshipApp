import { Component } from '@angular/core';
import { LucideAngularModule, Briefcase } from 'lucide-angular';

@Component({
  selector: 'app-footer',
  imports: [LucideAngularModule],
  templateUrl: './footer.html',
  styleUrl: './footer.scss'
})
export class Footer {
  logo = "";
  readonly briefCaseIcon = Briefcase;
}
