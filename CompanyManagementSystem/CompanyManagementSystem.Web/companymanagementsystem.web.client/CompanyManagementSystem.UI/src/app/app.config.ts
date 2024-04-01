import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import {
  provideCacheableAnimationLoader,
  provideLottieOptions,
} from 'ngx-lottie';
import { routes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimations(),
    provideHttpClient(),
    provideLottieOptions({
      player: () => import('lottie-web'),
    }),
    provideCacheableAnimationLoader(),
  ],
};
