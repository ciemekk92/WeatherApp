import { AstronomyDto } from '@weather-app/astronomy-models';

export type AstronomyState = {
  data: AstronomyDto | null;
  isLoadingData: boolean;
};
