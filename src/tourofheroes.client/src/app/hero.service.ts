import { Inject, Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HeroResponse, HeroesClient } from './api.generated.clients';
import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})
export class HeroService {
  private messageService = inject(MessageService);
  private heroesClient: HeroesClient;

  constructor(@Inject(HeroesClient) heroesClient: HeroesClient) {
        this.heroesClient = heroesClient;
  }

  getHeroes(): Observable<HeroResponse[]> {
    this.messageService.add('HeroService: fetched heroes');
    return this.heroesClient.getAll();
  }

  getHero(id: string): Observable<HeroResponse> {
    this.messageService.add(`HeroService: fetched hero id=${id}`);
    return this.heroesClient.get(id);
  }
}
