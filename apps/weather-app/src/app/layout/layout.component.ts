import { Component } from '@angular/core';
import { TopNavigationComponent } from '@weather-app/shared-ui';
import { RouterOutlet } from '@angular/router';
import { Toast } from 'primeng/toast';

@Component({
  selector: 'app-layout.component',
  imports: [TopNavigationComponent, RouterOutlet, Toast],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss',
})
export class LayoutComponent {}
