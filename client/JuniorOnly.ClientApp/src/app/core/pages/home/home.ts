import { Component } from '@angular/core';
import { LucideAngularModule, Search, MapPin, ChevronDown, Check  } from 'lucide-angular';
import { FormsModule } from '@angular/forms';
import { NgSelectComponent, NgSelectModule  } from '@ng-select/ng-select';
import { CommonModule } from '@angular/common';

interface Food {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-home',
  imports: [LucideAngularModule,
    FormsModule,
    NgSelectComponent,
    NgSelectModule,
    CommonModule
  ],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home {
  readonly searchIcon = Search;
  readonly mapIcon = MapPin;
  readonly chevronDownIcon = ChevronDown;
  selectedLocation?: string;
  items = [
    { name: 'Paris' },
    { name: 'Lyon' },
    { name: 'Marseille' },
    { name: 'Remote' }
  ];
}
