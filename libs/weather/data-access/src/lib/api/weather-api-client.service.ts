import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '@weather-app/shared-injection-tokens';
import { Observable } from 'rxjs';
import { WeatherDto } from '@weather-app/weather-models';

@Injectable({
  providedIn: 'root',
})
export class WeatherApiClientService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseApiUrl = inject(API_URL);

  getWeatherData(city: string): Observable<WeatherDto> {
    return this.httpClient.get<WeatherDto>(`${this.baseApiUrl}/weather/${city}`);
  }
}
