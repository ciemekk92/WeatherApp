import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DataSelectionComponent } from './data-selection.component';
import { Cities, cityDropdownOptions, DataSelection, DataType } from './data-selection.model';

describe('DataSelectionComponent', () => {
  let component: DataSelectionComponent;
  let fixture: ComponentFixture<DataSelectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DataSelectionComponent],
      schemas: [NO_ERRORS_SCHEMA],
    }).compileComponents();

    fixture = TestBed.createComponent(DataSelectionComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have default city set to first city option', () => {
    expect(component.selectedCity()).toEqual(cityDropdownOptions[0]);
  });

  it('should have default data type set to Weather', () => {
    expect(component.selectedDataType()).toBe(DataType.Weather);
  });

  it('should emit dataFetched with current selection when fetch button is clicked', () => {
    const emitted: DataSelection[] = [];
    const outputRef = fixture.debugElement.componentInstance.dataFetched;
    outputRef.subscribe((value: DataSelection) => emitted.push(value));

    const button = fixture.debugElement.query(By.css('p-button'));
    button.triggerEventHandler('onClick', {});
    fixture.detectChanges();

    expect(emitted.length).toBe(1);
    expect(emitted[0]).toEqual({
      city: cityDropdownOptions[0].value,
      dataType: DataType.Weather,
    });
  });

  it('should emit dataFetched with updated city when city model changes', () => {
    const emitted: DataSelection[] = [];
    const outputRef = fixture.debugElement.componentInstance.dataFetched;
    outputRef.subscribe((value: DataSelection) => emitted.push(value));

    component.selectedCity.set(cityDropdownOptions[1]);
    fixture.detectChanges();

    const button = fixture.debugElement.query(By.css('p-button'));
    button.triggerEventHandler('onClick', {});
    fixture.detectChanges();

    expect(emitted.length).toBe(1);
    expect(emitted[0].city).toBe(Cities.Dublin);
  });

  it('should emit dataFetched with updated data type when data type model changes', () => {
    const emitted: DataSelection[] = [];
    const outputRef = fixture.debugElement.componentInstance.dataFetched;
    outputRef.subscribe((value: DataSelection) => emitted.push(value));

    component.selectedDataType.set(DataType.Astronomy);
    fixture.detectChanges();

    const button = fixture.debugElement.query(By.css('p-button'));
    button.triggerEventHandler('onClick', {});
    fixture.detectChanges();

    expect(emitted.length).toBe(1);
    expect(emitted[0].dataType).toBe(DataType.Astronomy);
  });
});
