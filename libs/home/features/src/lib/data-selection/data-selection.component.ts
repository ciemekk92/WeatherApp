import { Component, model, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RadioButton } from 'primeng/radiobutton';
import { Button } from 'primeng/button';
import {
  Cities,
  cityDropdownOptions,
  DataSelection,
  DataType,
  dataTypeDropdownOptions,
} from './data-selection.model';
import { Select } from 'primeng/select';
import { DropdownOption } from '@weather-app/shared-utils';
import { Fieldset } from 'primeng/fieldset';
import { Panel } from 'primeng/panel';

@Component({
  selector: 'lib-home-features-data-selection',
  imports: [FormsModule, RadioButton, Button, Select, Fieldset, Panel],
  templateUrl: './data-selection.component.html',
  styleUrl: './data-selection.component.scss',
})
export class DataSelectionComponent {
  readonly dataFetched = output<DataSelection>();

  protected citiesOptions = cityDropdownOptions;
  protected dataTypeOptions = dataTypeDropdownOptions;

  selectedCity = model<DropdownOption<Cities>>(this.citiesOptions[0]);
  selectedDataType = model<DataType>(DataType.Weather);

  protected onFetchData(): void {
    this.dataFetched.emit({
      city: this.selectedCity().value,
      dataType: this.selectedDataType(),
    });
  }
}
