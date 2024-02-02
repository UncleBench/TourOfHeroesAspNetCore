import { NgFor } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { HeroResponse } from '../api.generated.clients';
import { HeroDetailComponent } from '../hero-detail/hero-detail.component';
import { HeroService } from '../hero.service';

@Component({
  selector: 'app-heroes',
  standalone: true,
  templateUrl: './heroes.component.html',
  styleUrl: './heroes.component.css',
  imports: [FormsModule, NgFor, HeroDetailComponent, RouterLink]
})
export class HeroesComponent implements OnInit {
  private heroService = inject(HeroService);
  
  heroes: HeroResponse[] = [];

  ngOnInit(): void {
    this.heroService.getHeroes()
      .subscribe(x => this.heroes = x);
  }
}
