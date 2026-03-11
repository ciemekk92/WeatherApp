import { Component, input } from '@angular/core';
import { AstronomyDto } from '@weather-app/astronomy-models';
import { Fieldset } from 'primeng/fieldset';

@Component({
  selector: 'app-astronomy-additional-info',
  standalone: true,
  templateUrl: './astronomy-additional-info.component.html',
  styleUrl: './astronomy-additional-info.component.scss',
  imports: [Fieldset],
})
export class AstronomyAdditionalInfoComponent {
  data = input.required<AstronomyDto>();
}
