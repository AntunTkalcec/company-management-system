import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, tap } from 'rxjs';
import { User } from '../interfaces/user';
import { Login } from '../interfaces/login';
import { UrlHelper } from '../helpers/url-helper';
import { StorageService } from './storage.service';
import { Register } from '../interfaces/register';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(
    private httpClient: HttpClient,
    private storage: StorageService
  ) {}

  public isAuthenticated(): boolean {
    const authData = this.storage.get('authData');

    if (!authData) return false;

    const helper = new JwtHelperService();
    const isExpired = helper.isTokenExpired(authData.accessToken);

    return !isExpired;
  }

  loginUser(formData: Login): Observable<HttpResponse<User>> {
    return this.httpClient
      .post<User>(`${UrlHelper.baseUrl}/authentication/login`, formData, {
        observe: 'response',
      })
      .pipe(
        tap((response: HttpResponse<User>): void =>
          this.saveAuthDataAndLogin(response.body)
        )
      );
  }

  registerUser(formData: Register): Observable<HttpResponse<User>> {
    return this.httpClient.post<User>(`${UrlHelper.baseUrl}/user`, formData, {
      observe: 'response',
    });
  }

  logoutUser(): void {
    this.storage.remove('authData');
    window.location.reload();
  }

  private saveAuthDataAndLogin(user: any): void {
    const accessToken: string = user.authenticationInfo.accessToken;
    const refreshToken: string = user.authenticationInfo.refreshToken;

    if (accessToken && refreshToken) {
      this.saveAuthDataAndUserInfo(user);
    }
  }

  private saveAuthDataAndUserInfo(user: User): void {
    this.storage.add('authData', user.authenticationInfo);
    this.storage.add('currentUser', user);
  }
}
