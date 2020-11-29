import { ErrorHandler, Injectable, NgZone } from '@angular/core';
import { ErrorService } from '../services/error.service';

@Injectable({
  providedIn: 'root',
})
export class GlobalErrorHandler implements ErrorHandler {
  constructor(private errorService: ErrorService, private ngZone: NgZone) {}

  handleError(error: any): void {
    this.ngZone.run(() => this.errorService.handleError(error));
  }
}
