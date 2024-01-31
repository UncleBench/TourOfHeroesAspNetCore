import { NgFor } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Hero } from '../hero';
import { HeroService } from '../hero.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
  imports: [NgFor, RouterLink]
})
export class DashboardComponent implements OnInit {
  private heroService = inject(HeroService);
  
  heroes: Hero[] = [];

  ngOnInit(): void {
    this.heroService.getHeroes()
      .subscribe(x => this.heroes = x.slice(1, 5));
  }
}