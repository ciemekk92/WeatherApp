import { z } from 'zod';

export const locationDtoSchema = z.object({
  name: z.string().nullable(),
  region: z.string().nullable(),
  country: z.string().nullable(),
  lat: z.number(),
  lon: z.number(),
  tzId: z.string().nullable(),
  localtimeEpoch: z.number(),
  localtime: z.string().nullable(),
});
