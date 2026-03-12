import { Component, input } from '@angular/core';
import { WeatherDto } from '@weather-app/weather-models';
import { Fieldset } from 'primeng/fieldset';

@Component({
  selector: 'lib-home-features-weather-additional-info',
  standalone: true,
  templateUrl: './weather-additional-info.component.html',
  styleUrl: './weather-additional-info.component.scss',
  imports: [Fieldset],
})
export class WeatherAdditionalInfoComponent {
  data = input.required<WeatherDto>();
}
