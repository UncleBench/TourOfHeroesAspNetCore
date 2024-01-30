import { Component, inject } from '@angular/core';
import { Title } from '@angular/platform-browser';

const appTitle: string = 'Tour of Heroes';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title: string;

  constructor() {
    let titleService: Title = inject(Title);
    titleService.setTitle(appTitle);
    this.title = titleService.getTitle();
  }
}
