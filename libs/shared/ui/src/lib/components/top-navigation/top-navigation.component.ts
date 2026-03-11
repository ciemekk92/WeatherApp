import { Component } from '@angular/core';
import { DarkModeSwitchComponent } from '../dark-mode-switch/dark-mode-switch.component';

@Component({
  selector: 'lib-shared-ui-top-navigation',
  imports: [DarkModeSwitchComponent],
  templateUrl: './top-navigation.component.html',
  styleUrl: './top-navigation.component.scss',
})
export class TopNavigationComponent {}
