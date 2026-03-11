import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DarkModeSwitchComponent } from './dark-mode-switch.component';
import { By } from '@angular/platform-browser';

describe('DarkModeSwitchComponent', () => {
  let component: DarkModeSwitchComponent;
  let fixture: ComponentFixture<DarkModeSwitchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DarkModeSwitchComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DarkModeSwitchComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should toggle dark mode class on html element', () => {
    const htmlElement = document.querySelector('html');
    const button = fixture.debugElement.query(
      By.css('p-togglebutton:last-of-type'),
    );

    expect(button).toBeTruthy();

    // Simulate clicking the dark mode button
    button.triggerEventHandler('onChange', { value: true });
    fixture.detectChanges();
    expect(htmlElement?.classList.contains('dark-mode')).toBe(true);
  });
});
