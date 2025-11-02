import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { provideZonelessChangeDetection } from '@angular/core';

import { GameService } from './game';
import { Game, CreateGameRequest, UpdateGameRequest } from '../models/game';
import { ConfigService } from '../config/config.service';

describe('GameService', () => {
  let service: GameService;
  let httpMock: HttpTestingController;
  let configService: jasmine.SpyObj<ConfigService>;

  beforeEach(() => {
    const configSpy = jasmine.createSpyObj('ConfigService', ['apiUrl'], {
      apiUrl: 'http://localhost:5058/api/GamesCatalog'
    });

    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        GameService,
        { provide: ConfigService, useValue: configSpy },
        provideZonelessChangeDetection()
      ]
    });
    service = TestBed.inject(GameService);
    httpMock = TestBed.inject(HttpTestingController);
    configService = TestBed.inject(ConfigService) as jasmine.SpyObj<ConfigService>;
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve all games', () => {
    const mockGames: Game[] = [
      { id: 1, title: 'Test Game', genre: 'Action', price: 59.99, releaseDate: '2023-01-01' },
      { id: 2, title: 'Another Game', genre: 'RPG', price: 49.99, releaseDate: '2023-02-01' }
    ];

    service.getAllGames().subscribe(games => {
      expect(games.length).toBe(2);
      expect(games).toEqual(mockGames);
    });

    const req = httpMock.expectOne('http://localhost:5058/api/GamesCatalog');
    expect(req.request.method).toBe('GET');
    req.flush(mockGames);
  });

  it('should retrieve a game by id', () => {
    const mockGame: Game = { id: 1, title: 'Test Game', genre: 'Action', price: 59.99, releaseDate: '2023-01-01' };

    service.getGameById(1).subscribe(game => {
      expect(game).toEqual(mockGame);
    });

    const req = httpMock.expectOne('http://localhost:5058/api/GamesCatalog/1');
    expect(req.request.method).toBe('GET');
    req.flush(mockGame);
  });

  it('should create a new game', () => {
    const createRequest: CreateGameRequest = { title: 'New Game', genre: 'Adventure', price: 39.99, releaseDate: '2023-03-01' };
    const mockGame: Game = { id: 3, ...createRequest };

    service.createGame(createRequest).subscribe(game => {
      expect(game).toEqual(mockGame);
    });

    const req = httpMock.expectOne('http://localhost:5058/api/GamesCatalog');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(createRequest);
    req.flush(mockGame);
  });

  it('should update an existing game', () => {
    const updateRequest: UpdateGameRequest = { id: 1, title: 'Updated Game', genre: 'Strategy', price: 29.99, releaseDate: '2023-04-01' };

    service.updateGame(1, updateRequest).subscribe(response => {
      expect(response).toBeNull();
    });

    const req = httpMock.expectOne('http://localhost:5058/api/GamesCatalog/1');
    expect(req.request.method).toBe('PUT');
    expect(req.request.body).toEqual(updateRequest);
    req.flush(null);
  });

  it('should delete a game', () => {
    service.deleteGame(1).subscribe(response => {
      expect(response).toBeNull();
    });

    const req = httpMock.expectOne('http://localhost:5058/api/GamesCatalog/1');
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });
});
