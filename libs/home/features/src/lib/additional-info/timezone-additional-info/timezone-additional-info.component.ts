import { Component, computed, input } from '@angular/core';
import { TimezoneDto } from '@weather-app/timezone-models';
import { Fieldset } from 'primeng/fieldset';

@Component({
  selector: 'lib-home-features-timezone-additional-info',
  standalone: true,
  templateUrl: './timezone-additional-info.component.html',
  styleUrl: './timezone-additional-info.component.scss',
  imports: [Fieldset],
})
export class TimezoneAdditionalInfoComponent {
  data = input.required<TimezoneDto>();

  formattedCoordinates = computed(() => {
    const { lat, lon } = this.data().location;
    const latDirection = lat >= 0 ? 'N' : 'S';
    const lonDirection = lon >= 0 ? 'E' : 'W';

    return `${Math.abs(lat).toFixed(2)}° ${latDirection}, ${Math.abs(lon).toFixed(2)}° ${lonDirection}`;
  });
}
