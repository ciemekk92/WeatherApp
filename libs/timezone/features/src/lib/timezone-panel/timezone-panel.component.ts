import { Component, input } from '@angular/core';
import { TimezoneDto } from '@weather-app/timezone-models';

@Component({
  selector: 'lib-timezone-features-timezone-panel',
  standalone: true,
  templateUrl: './timezone-panel.component.html',
  styleUrl: './timezone-panel.component.scss',
})
export class TimezonePanelComponent {
  data = input.required<TimezoneDto>();
}
