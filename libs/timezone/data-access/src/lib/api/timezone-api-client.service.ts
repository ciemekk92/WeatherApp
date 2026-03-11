import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '@weather-app/shared-injection-tokens';
import { Observable } from 'rxjs';
import { TimezoneDto } from '@weather-app/timezone-models';

@Injectable({
  providedIn: 'root',
})
export class TimezoneApiClientService {
  private readonly httpClient = inject(HttpClient);
  private readonly baseApiUrl = inject(API_URL);

  getTimezoneData(city: string): Observable<TimezoneDto> {
    return this.httpClient.get<TimezoneDto>(`${this.baseApiUrl}/timezone/${city}`);
  }
}
