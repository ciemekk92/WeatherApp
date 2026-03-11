import { WeatherDto } from '@weather-app/weather-models';

export type WeatherState = {
  data: WeatherDto | null;
  isLoadingData: boolean;
};
