import { DropdownOption } from '@weather-app/shared-utils';

export enum DataType {
  Weather = 'weather',
  Astronomy = 'astronomy',
  Timezone = 'timezone',
}

export enum Cities {
  Vancouver = 'Vancouver',
  Dublin = 'Dublin',
  Sydney = 'Sydney',
}

export const dataTypeDropdownOptions: DropdownOption[] = [
  { name: 'Weather', value: DataType.Weather },
  { name: 'Astronomy', value: DataType.Astronomy },
  { name: 'Timezone', value: DataType.Timezone },
];

export const cityDropdownOptions: DropdownOption<Cities>[] = Object.values(Cities).map((city) => ({
  name: city,
  value: city,
}));

export interface DataSelection {
  city: Cities;
  dataType: DataType;
}
