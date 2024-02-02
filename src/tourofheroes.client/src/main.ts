import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { bootstrapApplication } from '@angular/platform-browser';
import { PreloadAllModules, provideRouter, withPreloading } from '@angular/router';
import { API_BASE_URL, HeroesClient } from './app/api.generated.clients';
import { AppComponent } from './app/app.component';
import { APP_ROUTES } from './app/app.routes';
import { environment } from './environments/environment';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(APP_ROUTES,
      withPreloading(PreloadAllModules),
      //withDebugTracing(),
    ),
    provideHttpClient(withInterceptorsFromDi()),
    {
      provide: API_BASE_URL,
      useValue: environment.apiRoot
    },
    HeroesClient
  ]
}).catch(err => console.error(err));
