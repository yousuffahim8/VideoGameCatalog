export interface AppConfig {
  apiUrl: string;
  production: boolean;
}

export const environment: AppConfig = {
  apiUrl: 'http://localhost:5058/api/GamesCatalog',
  production: false
};

// For production, you would typically have a separate environment file
export const environmentProd: AppConfig = {
  apiUrl: 'https://your-production-api.com/api/GamesCatalog',
  production: true
};