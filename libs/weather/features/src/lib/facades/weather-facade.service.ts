import { Injectable, inject, signal, computed } from '@angular/core';
import { WeatherApiClientService } from '@weather-app/weather-data-access';
import { WeatherState } from './weather-state.model';
import { finalize, Observable, tap } from 'rxjs';
import { WeatherDto } from '@weather-app/weather-models';

@Injectable({
  providedIn: 'root',
})
export class WeatherFacadeService {
  readonly $weatherData = computed(() => this.weatherStore().data);
  readonly $isLoadingWeatherData = computed(() => this.weatherStore().isLoadingData);

  private readonly weatherApiClientService = inject(WeatherApiClientService);

  private weatherStore = signal<WeatherState>({
    data: null,
    isLoadingData: false,
  });

  loadWeatherData(city: string): Observable<WeatherDto> {
    this.setLoadingWeatherData(true);

    return this.weatherApiClientService.getWeatherData(city).pipe(
      tap((data) => {
        this.weatherStore.update((state) => ({
          ...state,
          data,
        }));
      }),
      finalize(() => this.setLoadingWeatherData(false)),
    );
  }

  private setLoadingWeatherData(isLoading: boolean): void {
    this.weatherStore.update((state) => ({
      ...state,
      isLoadingData: isLoading,
    }));
  }
}
