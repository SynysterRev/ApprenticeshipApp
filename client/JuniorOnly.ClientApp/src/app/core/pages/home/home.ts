import { Component } from '@angular/core';
import { LucideAngularModule, Search, MapPin, ChevronDown  } from 'lucide-angular';
import { FormsModule } from '@angular/forms';
import { NgLabelTemplateDirective, NgOptionTemplateDirective, NgSelectComponent, NgSelectModule  } from '@ng-select/ng-select';

interface Food {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-home',
  imports: [LucideAngularModule,
    FormsModule,
    NgLabelTemplateDirective,
    NgOptionTemplateDirective,
    NgSelectComponent,
    NgSelectModule 
  ],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home {
  readonly searchIcon = Search;
  readonly mapIcon = MapPin;
  readonly chevronDownIcon = ChevronDown;
  selectedLocation?: string;
  foods: Food[] = [
    {value: 'steak-0', viewValue: 'Steak'},
    {value: 'pizza-1', viewValue: 'Pizza'},
    {value: 'tacos-2', viewValue: 'Tacos'},
  ];
}
