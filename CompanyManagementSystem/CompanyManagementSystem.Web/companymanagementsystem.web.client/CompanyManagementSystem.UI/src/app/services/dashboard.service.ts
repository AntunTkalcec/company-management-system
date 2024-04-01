import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StorageService } from './storage.service';
import { User } from '../interfaces/user';
import { Observable } from 'rxjs';
import { Company } from '../interfaces/company';
import { UrlHelper } from '../helpers/url-helper';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  currentUser: User = this.storage.get('currentUser');

  constructor(
    private httpClient: HttpClient,
    private storage: StorageService
  ) {}

  getCompanyInfo(): Observable<Company> {
    return this.httpClient.get<Company>(
      `${UrlHelper.baseUrl}/company/${this.currentUser.companyId}`
    );
  }
}
