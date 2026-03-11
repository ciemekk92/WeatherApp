import { z } from 'zod';
import { locationDtoSchema } from '@weather-app/shared-models';

export const timezoneDtoSchema = z.object({
  location: locationDtoSchema,
});

export type TimezoneDto = z.infer<typeof timezoneDtoSchema>;
