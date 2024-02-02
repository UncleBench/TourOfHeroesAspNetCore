import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterLinkWithHref, RouterOutlet } from '@angular/router';
import { HeroesComponent } from './heroes/heroes.component';
import { MessagesComponent } from './messages/messages.component';

const APPTITLE: string = 'Tour of Heroes';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [HeroesComponent, MessagesComponent, RouterOutlet, RouterLinkWithHref ]
})
export class AppComponent {
  title: string;

  constructor() {
    let titleService: Title = inject(Title);
    titleService.setTitle(APPTITLE);
    this.title = titleService.getTitle();
  }
}
