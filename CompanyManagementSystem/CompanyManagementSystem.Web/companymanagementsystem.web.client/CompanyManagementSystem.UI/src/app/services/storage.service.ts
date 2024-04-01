import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class StorageService {
  add(key: string, data: any): void {
    localStorage.setItem(key, JSON.stringify(data));
  }

  get(key: string): any {
    const data: string | null = localStorage.getItem(key);

    if (data && data != 'undefined') {
      return JSON.parse(data);
    }

    return null;
  }

  remove(key: string): void {
    localStorage.removeItem(key);
  }
}
