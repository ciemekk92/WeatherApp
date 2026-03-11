import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '@weather-app/shared-injection-tokens';
import { Observable } from 'rxjs';
import { AstronomyDto } from '@weather-app/astronomy-models';

@Injectable({
  providedIn: 'root',
})
export class AstronomyApiClientService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseApiUrl = inject(API_URL);

  getAstronomyData(city: string): Observable<AstronomyDto> {
    return this.httpClient.get<AstronomyDto>(
      `${this.baseApiUrl}/astronomy/${city}`,
    );
  }
}
