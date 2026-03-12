import { HttpClient, provideHttpClient, withInterceptors } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { providePrimeNgServices, ToastService } from '@weather-app/shared-services';
import { TestBed } from '@angular/core/testing';
import { NotFoundExceptionInterceptor } from './not-found-exception.interceptor';

describe('NotFoundExceptionInterceptor', () => {
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;
  let toastService: ToastService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        ToastService,
        provideHttpClient(withInterceptors([NotFoundExceptionInterceptor])),
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

  it('should show warn toast on NotFoundException', async () => {
    // Given
    vi.spyOn(toastService, 'showWarn');

    // When
    httpClient.get('/api/test').subscribe({
      error: () => {
        // expected to fail
      },
    });

    // Then
    const req = httpTestingController.expectOne('/api/test');
    const requestBody = {
      type: 'NotFound',
      status: 404,
      detail: 'Test not found message',
    };
    const requestOpts = { status: 404, statusText: 'Not Found' };

    req.flush(requestBody, requestOpts);

    expect(toastService.showWarn).toHaveBeenCalledWith(requestBody.detail);
  });
});
