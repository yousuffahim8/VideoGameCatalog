import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { GameService } from '../../services/game';
import { Game } from '../../models/game';

@Component({
  selector: 'app-game-list',
  imports: [CommonModule, RouterModule],
  templateUrl: './game-list.html',
  styleUrl: './game-list.css',
})
export class GameList implements OnInit {
  games = signal<Game[]>([]);
  loading = signal(true);
  error = signal<string | null>(null);

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    this.loadGames();
  }

  loadGames(): void {
    this.loading.set(true);
    this.error.set(null);
    
    this.gameService.getAllGames().subscribe({
      next: (games) => {
        this.games.set(games);
        this.loading.set(false);
      },
      error: (error) => {
        this.error.set(error);
        this.loading.set(false);
      }
    });
  }

  deleteGame(id: number): void {
    if (confirm('Are you sure you want to delete this game?')) {
      this.gameService.deleteGame(id).subscribe({
        next: () => {
          this.loadGames(); // Refresh the list
        },
        error: (error) => {
          this.error.set(error);
        }
      });
    }
  }

  formatPrice(price: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD'
    }).format(price);
  }

  formatDate(dateString: string): string {
    return new Date(dateString).toLocaleDateString();
  }
}
