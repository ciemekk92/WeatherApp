import { Component, model } from '@angular/core';
import { SelectButton, SelectButtonChangeEvent } from 'primeng/selectbutton';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'lib-shared-ui-dark-mode-switch',
  imports: [SelectButton, FormsModule],
  templateUrl: './dark-mode-switch.component.html',
})
export class DarkModeSwitchComponent {
  protected value = model(false);

  protected options = [
    { label: 'Light', value: false, icon: '☀️' },
    { label: 'Dark', value: true, icon: '🌒' },
  ];

  protected handleChange(event: SelectButtonChangeEvent): void {
    const isDarkMode = event.value;
    const element = document.querySelector('html');

    if (isDarkMode) {
      element?.classList.add('dark-mode');
    } else {
      element?.classList.remove('dark-mode');
    }
  }
}
