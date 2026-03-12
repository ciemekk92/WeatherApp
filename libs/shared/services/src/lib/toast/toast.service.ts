import { inject, Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

enum ToastSeverity {
  Success = 'success',
  Info = 'info',
  Warn = 'warn',
  Error = 'error',
}

enum ToastSummary {
  Success = 'Success',
  Info = 'Info',
  Warn = 'Warning',
  Error = 'Error',
}

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  private readonly messageService = inject(MessageService);

  showSuccess(message: string): void {
    this.messageService.add({
      severity: ToastSeverity.Success,
      summary: ToastSummary.Success,
      detail: message,
    });
  }

  showInfo(message: string): void {
    this.messageService.add({
      severity: ToastSeverity.Info,
      summary: ToastSummary.Info,
      detail: message,
    });
  }

  showWarn(message: string): void {
    this.messageService.add({
      severity: ToastSeverity.Warn,
      summary: ToastSummary.Warn,
      detail: message,
    });
  }

  showError(message: string): void {
    this.messageService.add({
      severity: ToastSeverity.Error,
      summary: ToastSummary.Error,
      detail: message,
    });
  }
}
