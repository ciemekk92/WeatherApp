import { HttpInterceptorFn, HttpStatusCode } from '@angular/common/http';
import { inject } from '@angular/core';
import { ToastService } from '@weather-app/shared-services';
import { catchError, throwError } from 'rxjs';

export const AppLogicExceptionInterceptor: HttpInterceptorFn = (req, next) => {
  const toastService = inject(ToastService);

  return next(req).pipe(
    catchError((errorResponse) => {
      const error = errorResponse?.error
        ? errorResponse.error
        : typeof errorResponse === 'string'
          ? parseStringResponse(errorResponse)
          : errorResponse;

      if (
        error &&
        error.type === 'ApplicationLogicException' &&
        error.status === HttpStatusCode.BadRequest
      ) {
        toastService.showError(error.detail);
      }

      return throwError(() => errorResponse);
    }),
  );

  function parseStringResponse(errorResponse: string): unknown {
    try {
      return JSON.parse(errorResponse);
    } catch (e) {
      console.error(e);
      return throwError(() => errorResponse);
    }
  }
};
