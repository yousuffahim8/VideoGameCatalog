export interface Game {
  id: number;
  title: string;
  genre: string;
  price: number;
  releaseDate: string; // ISO date string from API
}

export interface CreateGameRequest {
  title: string;
  genre: string;
  price: number;
  releaseDate: string;
}

export interface UpdateGameRequest extends CreateGameRequest {
  id: number;
}