import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: '/games',
    pathMatch: 'full'
  },
  {
    path: 'games',
    loadComponent: () => import('./pages/game-list/game-list').then(m => m.GameList)
  },
  {
    path: 'games/new',
    loadComponent: () => import('./pages/game-edit/game-edit').then(m => m.GameEdit)
  },
  {
    path: 'games/:id',
    loadComponent: () => import('./pages/game-edit/game-edit').then(m => m.GameEdit)
  },
  {
    path: '**',
    redirectTo: '/games'
  }
];
