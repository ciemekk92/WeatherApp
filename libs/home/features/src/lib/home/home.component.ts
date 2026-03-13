import { Component, computed, inject, signal } from '@angular/core';
import { Observable, take } from 'rxjs';
import { WeatherFacadeService, WeatherPanelComponent } from '@weather-app/weather-features';
import { AstronomyFacadeService, AstronomyPanelComponent } from '@weather-app/astronomy-features';
import { TimezoneFacadeService, TimezonePanelComponent } from '@weather-app/timezone-features';
import { DataSelectionComponent } from '../data-selection/data-selection.component';
import { DataSelection, DataType } from '../data-selection/data-selection.model';
import { WeatherAdditionalInfoComponent } from '../additional-info/weather-additional-info/weather-additional-info.component';
import { AstronomyAdditionalInfoComponent } from '../additional-info/astronomy-additional-info/astronomy-additional-info.component';
import { TimezoneAdditionalInfoComponent } from '../additional-info/timezone-additional-info/timezone-additional-info.component';
import { LoaderComponent } from '@weather-app/shared-ui';

@Component({
  selector: 'lib-home-features-home',
  imports: [
    DataSelectionComponent,
    WeatherPanelComponent,
    AstronomyPanelComponent,
    TimezonePanelComponent,
    WeatherAdditionalInfoComponent,
    AstronomyAdditionalInfoComponent,
    TimezoneAdditionalInfoComponent,
    LoaderComponent,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
  private readonly weatherFacade = inject(WeatherFacadeService);
  private readonly astronomyFacade = inject(AstronomyFacadeService);
  private readonly timezoneFacade = inject(TimezoneFacadeService);

  readonly currentDataType = signal<DataType | null>(null);

  readonly currentWeatherData = computed(() =>
    this.currentDataType() === DataType.Weather ? this.weatherFacade.$weatherData() : null,
  );

  readonly isLoadingWeatherData = this.weatherFacade.$isLoadingWeatherData;
  readonly isLoadingAstronomyData = this.astronomyFacade.$isLoadingAstronomyData;
  readonly isLoadingTimezoneData = this.timezoneFacade.$isLoadingTimezoneData;

  readonly currentAstronomyData = computed(() =>
    this.currentDataType() === DataType.Astronomy ? this.astronomyFacade.$astronomyData() : null,
  );

  readonly currentTimezoneData = computed(() =>
    this.currentDataType() === DataType.Timezone ? this.timezoneFacade.$timezoneData() : null,
  );

  readonly isLoading = computed(
    () =>
      this.isLoadingWeatherData() || this.isLoadingAstronomyData() || this.isLoadingTimezoneData(),
  );

  onDataFetched(selection: DataSelection): void {
    this.currentDataType.set(selection.dataType);

    let request: Observable<unknown>;

    switch (selection.dataType) {
      case DataType.Weather:
        request = this.weatherFacade.loadWeatherData(selection.city);
        break;
      case DataType.Astronomy:
        request = this.astronomyFacade.loadAstronomyData(selection.city);
        break;
      case DataType.Timezone:
        request = this.timezoneFacade.loadTimezoneData(selection.city);
        break;
    }

    request.pipe(take(1)).subscribe();
  }
}
