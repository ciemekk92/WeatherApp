import { Injectable, inject, signal, computed } from '@angular/core';
import { TimezoneApiClientService } from '@weather-app/timezone-data-access';
import { TimezoneState } from './timezone-state.model';
import { finalize, Observable, tap } from 'rxjs';
import { TimezoneDto } from '@weather-app/timezone-models';

@Injectable({
  providedIn: 'root',
})
export class TimezoneFacadeService {
  readonly $timezoneData = computed(() => this.timezoneStore().data);
  readonly $isLoadingTimezoneData = computed(() => this.timezoneStore().isLoadingData);

  private readonly timezoneApiClientService = inject(TimezoneApiClientService);

  private timezoneStore = signal<TimezoneState>({
    data: null,
    isLoadingData: false,
  });

  loadTimezoneData(city: string): Observable<TimezoneDto> {
    this.setLoadingTimezoneData(true);

    return this.timezoneApiClientService.getTimezoneData(city).pipe(
      tap((data) => {
        this.timezoneStore.update((state) => ({
          ...state,
          data,
        }));
      }),
      finalize(() => this.setLoadingTimezoneData(false)),
    );
  }

  private setLoadingTimezoneData(isLoading: boolean): void {
    this.timezoneStore.update((state) => ({
      ...state,
      isLoadingData: isLoading,
    }));
  }
}
