import { Component, input } from '@angular/core';
import { AstronomyDto } from '@weather-app/astronomy-models';

@Component({
  selector: 'lib-astronomy-features-astronomy-panel',
  templateUrl: './astronomy-panel.component.html',
  styleUrl: './astronomy-panel.component.scss',
})
export class AstronomyPanelComponent {
  data = input.required<AstronomyDto>();
}
