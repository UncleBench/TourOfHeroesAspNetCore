import { NgFor } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Hero } from '../hero';
import { HeroDetailComponent } from '../hero-detail/hero-detail.component';
import { HeroService } from '../hero.service';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-heroes',
  standalone: true,
  templateUrl: './heroes.component.html',
  styleUrl: './heroes.component.css',
  imports: [
    FormsModule,
    NgFor,
    HeroDetailComponent
  ],
})
export class HeroesComponent implements OnInit {
  private heroService = inject(HeroService);
  private messageService = inject(MessageService);

  heroes: Hero[] = [];
  selectedHero?: Hero;

  ngOnInit(): void {
    this.heroService.getHeroes()
      .subscribe(x => this.heroes = x);
  }

  onSelect(hero: Hero): void {
    if (this.selectedHero === hero)
      return;

    this.selectedHero = hero;
    this.messageService.add(`HeroesComponent: Selected hero id=${hero.id}`);
  }
}
