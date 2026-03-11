import { Route } from '@angular/router';

export const appRoutes: Route[] = [
  {
    path: '',
    loadComponent: async () => (await import('./layout/layout.component')).LayoutComponent,
    children: [
      {
        path: '',
        loadComponent: async () => (await import('@weather-app/home-features')).HomeComponent,
      },
    ],
  },
];
