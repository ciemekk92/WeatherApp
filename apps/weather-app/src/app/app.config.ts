import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { appRoutes } from './app.routes';
import { providePrimeNG } from 'primeng/config';
import Lara from '@primeuix/themes/lara';
import { API_URL } from '@weather-app/shared-injection-tokens';
import { AppLogicExceptionInterceptor, NotFoundExceptionInterceptor } from '@weather-app/shared-ui';
import { environment } from '../environments/environment';
import { providePrimeNgServices } from '@weather-app/shared-services';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(appRoutes),
    provideHttpClient(
      withInterceptors([AppLogicExceptionInterceptor, NotFoundExceptionInterceptor]),
    ),
    providePrimeNgServices(),
    providePrimeNG({
      theme: {
        preset: Lara,
        options: {
          darkModeSelector: '.dark-mode',
        },
      },
    }),
    { provide: API_URL, useValue: environment.apiUrl },
  ],
};
