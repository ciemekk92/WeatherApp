import { z } from 'zod';
import { locationDtoSchema } from '@weather-app/shared-models';

export const conditionDtoSchema = z.object({
  text: z.string().nullable(),
  icon: z.string().nullable(),
  code: z.number().int(),
});

export const weatherDataDtoSchema = z.object({
  lastUpdatedEpoch: z.number(),
  lastUpdated: z.string().nullable(),
  tempC: z.number(),
  tempF: z.number(),
  isDay: z.boolean(),
  condition: conditionDtoSchema.nullable(),
  windMph: z.number(),
  windKph: z.number(),
  windDegree: z.number().int(),
  windDir: z.string().nullable(),
  pressureMb: z.number(),
  pressureIn: z.number(),
  precipMm: z.number(),
  precipIn: z.number(),
  humidity: z.number().int(),
  cloud: z.number().int(),
  feelslikeC: z.number(),
  feelslikeF: z.number(),
  windchillC: z.number(),
  windchillF: z.number(),
  heatindexC: z.number(),
  heatindexF: z.number(),
  dewpointC: z.number(),
  dewpointF: z.number(),
  visKm: z.number(),
  visMiles: z.number(),
  uv: z.number(),
  gustMph: z.number(),
  gustKph: z.number(),
});

export const weatherDtoSchema = z.object({
  location: locationDtoSchema,
  current: weatherDataDtoSchema,
});

export type WeatherDto = z.infer<typeof weatherDtoSchema>;
