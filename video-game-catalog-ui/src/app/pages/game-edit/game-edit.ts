import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { GameService } from '../../services/game';
import { Game, CreateGameRequest, UpdateGameRequest } from '../../models/game';

@Component({
  selector: 'app-game-edit',
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './game-edit.html',
  styleUrl: './game-edit.css',
})
export class GameEdit implements OnInit {
  gameForm: FormGroup;
  isEditMode = signal(false);
  gameId = signal<number | null>(null);
  loading = signal(false);
  error = signal<string | null>(null);
  saving = signal(false);

  constructor(
    private fb: FormBuilder,
    private gameService: GameService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.gameForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
      genre: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(50)]],
      price: [0, [Validators.required, Validators.min(0), Validators.max(9999.99)]],
      releaseDate: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if (id && id !== 'new') {
      this.isEditMode.set(true);
      this.gameId.set(+id);
      this.loadGame(+id);
    }
  }

  loadGame(id: number): void {
    this.loading.set(true);
    this.error.set(null);

    this.gameService.getGameById(id).subscribe({
      next: (game) => {
        this.populateForm(game);
        this.loading.set(false);
      },
      error: (error) => {
        this.error.set(error);
        this.loading.set(false);
      }
    });
  }

  populateForm(game: Game): void {
    const releaseDate = new Date(game.releaseDate).toISOString().split('T')[0];
    
    this.gameForm.patchValue({
      title: game.title,
      genre: game.genre,
      price: game.price,
      releaseDate: releaseDate
    });
  }

  onSubmit(): void {
    if (this.gameForm.valid) {
      this.saving.set(true);
      this.error.set(null);

      const formValue = this.gameForm.value;
      
      if (this.isEditMode()) {
        const updateRequest: UpdateGameRequest = {
          id: this.gameId()!,
          title: formValue.title,
          genre: formValue.genre,
          price: formValue.price,
          releaseDate: formValue.releaseDate
        };

        this.gameService.updateGame(this.gameId()!, updateRequest).subscribe({
          next: () => {
            this.router.navigate(['/games']);
          },
          error: (error) => {
            this.error.set(error);
            this.saving.set(false);
          }
        });
      } else {
        const createRequest: CreateGameRequest = {
          title: formValue.title,
          genre: formValue.genre,
          price: formValue.price,
          releaseDate: formValue.releaseDate
        };

        this.gameService.createGame(createRequest).subscribe({
          next: () => {
            this.router.navigate(['/games']);
          },
          error: (error) => {
            this.error.set(error);
            this.saving.set(false);
          }
        });
      }
    } else {
      this.markFormGroupTouched();
    }
  }

  onCancel(): void {
    this.router.navigate(['/games']);
  }

  private markFormGroupTouched(): void {
    Object.keys(this.gameForm.controls).forEach(key => {
      const control = this.gameForm.get(key);
      control?.markAsTouched();
    });
  }

  getFieldError(fieldName: string): string | null {
    const field = this.gameForm.get(fieldName);
    if (field && field.invalid && (field.dirty || field.touched)) {
      if (field.errors?.['required']) {
        return `${this.getFieldLabel(fieldName)} is required`;
      }
      if (field.errors?.['minlength']) {
        return `${this.getFieldLabel(fieldName)} is too short`;
      }
      if (field.errors?.['maxlength']) {
        return `${this.getFieldLabel(fieldName)} is too long`;
      }
      if (field.errors?.['min']) {
        return `${this.getFieldLabel(fieldName)} must be at least ${field.errors['min'].min}`;
      }
      if (field.errors?.['max']) {
        return `${this.getFieldLabel(fieldName)} must be at most ${field.errors['max'].max}`;
      }
    }
    return null;
  }

  private getFieldLabel(fieldName: string): string {
    const labels: { [key: string]: string } = {
      title: 'Title',
      genre: 'Genre',
      price: 'Price',
      releaseDate: 'Release Date'
    };
    return labels[fieldName] || fieldName;
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.gameForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }
}
