import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideZonelessChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';

import { GameEdit } from './game-edit';

describe('GameEdit', () => {
  let component: GameEdit;
  let fixture: ComponentFixture<GameEdit>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GameEdit],
      providers: [
        provideZonelessChangeDetection(),
        provideRouter([]),
        provideHttpClient()
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GameEdit);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
