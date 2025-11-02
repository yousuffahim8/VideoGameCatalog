import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private config = environment;

  get apiUrl(): string {
    return this.config.apiUrl;
  }

  get isProduction(): boolean {
    return this.config.production;
  }

  getApiEndpoint(resource: string = ''): string {
    return resource ? `${this.config.apiUrl}/${resource}` : this.config.apiUrl;
  }
}