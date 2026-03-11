import { z } from 'zod';
import { locationDtoSchema } from '@weather-app/shared-models';

export const astroDtoSchema = z.object({
  sunrise: z.string().nullable(),
  sunset: z.string().nullable(),
  moonrise: z.string().nullable(),
  moonset: z.string().nullable(),
  moonPhase: z.string().nullable(),
  moonIllumination: z.number().int(),
  isMoonUp: z.boolean(),
  isSunUp: z.boolean(),
});

export const astronomyDtoSchema = z.object({
  location: locationDtoSchema,
  astronomy: z.object({
    astro: astroDtoSchema,
  }),
});

export type AstronomyDto = z.infer<typeof astronomyDtoSchema>;
