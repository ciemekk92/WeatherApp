import { TimezoneDto } from '@weather-app/timezone-models';

export type TimezoneState = {
  data: TimezoneDto | null;
  isLoadingData: boolean;
};
