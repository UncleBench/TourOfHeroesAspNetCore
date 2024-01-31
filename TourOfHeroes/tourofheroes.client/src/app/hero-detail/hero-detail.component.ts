import { Location, NgIf, UpperCasePipe } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Hero } from '../hero';
import { HeroService } from '../hero.service';

@Component({
  selector: 'app-hero-detail',
  standalone: true,
  templateUrl: './hero-detail.component.html',
  styleUrl: './hero-detail.component.css',
  imports: [FormsModule, NgIf, UpperCasePipe]
})
export class HeroDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private heroService = inject(HeroService);
  private location = inject(Location);
  
  hero: Hero | undefined;
  
  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.heroService.getHero(id)
      .subscribe(x => this.hero = x);
  }

  goBack(): void {
    this.location.back();
  }
}
