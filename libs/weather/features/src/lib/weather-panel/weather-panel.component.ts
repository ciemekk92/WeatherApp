import { Component, input } from '@angular/core';
import { WeatherDto } from '@weather-app/weather-models';

@Component({
  selector: 'lib-weather-features-weather-panel',
  templateUrl: './weather-panel.component.html',
  styleUrl: './weather-panel.component.scss',
})
export class WeatherPanelComponent {
  data = input.required<WeatherDto>();
}
