import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IApiResponse } from '../models/ApiResponse';
import { IUser } from '../models/User';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = environment.apiUrl;

  private currentUserSource = new BehaviorSubject<IUser>(null);
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private http: HttpClient) {}

  login(values: { username: string; password: string }): Observable<IUser> {
    return this.http
      .post<IApiResponse<IUser>>(this.baseUrl + 'Auth/SignIn', values)
      .pipe(
        map((userResponse: IApiResponse<IUser>) => {
          const user = userResponse.result;
          this.currentUserSource.next(user);
          return user;
        })
      );
  }
}
