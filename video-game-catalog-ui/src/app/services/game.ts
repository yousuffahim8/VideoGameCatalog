import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Game, CreateGameRequest, UpdateGameRequest } from '../models/game';
import { ConfigService } from '../config/config.service';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  private readonly apiUrl: string;

  constructor(
    private http: HttpClient,
    private configService: ConfigService
  ) {
    this.apiUrl = this.configService.apiUrl;
  }

  getAllGames(): Observable<Game[]> {
    return this.http.get<Game[]>(this.apiUrl)
      .pipe(catchError(this.handleError));
  }

  getGameById(id: number): Observable<Game> {
    return this.http.get<Game>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  createGame(game: CreateGameRequest): Observable<Game> {
    return this.http.post<Game>(this.apiUrl, game)
      .pipe(catchError(this.handleError));
  }

  updateGame(id: number, game: UpdateGameRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, game)
      .pipe(catchError(this.handleError));
  }

  deleteGame(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred';
    
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    
    console.error(errorMessage);
    return throwError(() => errorMessage);
  }
}


