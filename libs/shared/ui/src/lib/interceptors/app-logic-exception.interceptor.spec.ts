import { HttpClient, provideHttpClient, withInterceptors } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { providePrimeNgServices, ToastService } from '@weather-app/shared-services';
import { TestBed } from '@angular/core/testing';
import { AppLogicExceptionInterceptor } from './app-logic-exception.interceptor';

describe('AppLogicExceptionInterceptor', () => {
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;
  let toastService: ToastService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ToastService,
        provideHttpClient(withInterceptors([AppLogicExceptionInterceptor])),
        provideHttpClientTesting(),
        providePrimeNgServices(),
      ],
    });

    httpClient = TestBed.inject(HttpClient);
    httpTestingController = TestBed.inject(HttpTestingController);
    toastService = TestBed.inject(ToastService);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should show error toast on ApplicationLogicException', async () => {
    // Given
    vi.spyOn(toastService, 'showError');

    // When
    httpClient.get('/api/test').subscribe({
      error: () => {
        // expected to fail
      },
    });

    // Then
    const req = httpTestingController.expectOne('/api/test');
    const requestBody = {
      type: 'ApplicationLogicException',
      status: 400,
      detail: 'Test error message',
    };
    const requestOpts = { status: 400, statusText: 'Bad Request' };

    req.flush(requestBody, requestOpts);

    expect(toastService.showError).toHaveBeenCalledWith(requestBody.detail);
  });
});
