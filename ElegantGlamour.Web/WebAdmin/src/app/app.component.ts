import { Component } from '@angular/core';
import { IUser } from './core/models/User';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  user: IUser;
  title = 'WebAdmin';

  constructor(private authService: AuthService) {
    this.authService.currentUser$.subscribe((user) => (this.user = user));
  }
}
