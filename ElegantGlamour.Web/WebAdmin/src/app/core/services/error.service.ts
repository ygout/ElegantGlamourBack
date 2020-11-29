import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ErrorService {
  constructor() {}

  handleError(error: Error): void {
    console.error(error);
  }
}
