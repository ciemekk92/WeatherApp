import { Injectable, inject, signal, computed } from '@angular/core';
import { AstronomyApiClientService } from '@weather-app/astronomy-data-access';
import { AstronomyState } from './astronomy-state.model';
import { finalize, Observable, tap } from 'rxjs';
import { AstronomyDto } from '@weather-app/astronomy-models';

@Injectable({
  providedIn: 'root',
})
export class AstronomyFacadeService {
  readonly $astronomyData = computed(() => this.astronomyStore().data);
  readonly $isLoadingAstronomyData = computed(() => this.astronomyStore().isLoadingData);

  private readonly astronomyApiClientService = inject(AstronomyApiClientService);

  private astronomyStore = signal<AstronomyState>({
    data: null,
    isLoadingData: false,
  });

  loadAstronomyData(city: string): Observable<AstronomyDto> {
    this.setLoadingAstronomyData(true);

    return this.astronomyApiClientService.getAstronomyData(city).pipe(
      tap((data) => {
        this.astronomyStore.update((state) => ({
          ...state,
          data,
        }));
      }),
      finalize(() => this.setLoadingAstronomyData(false)),
    );
  }

  private setLoadingAstronomyData(isLoading: boolean): void {
    this.astronomyStore.update((state) => ({
      ...state,
      isLoadingData: isLoading,
    }));
  }
}
