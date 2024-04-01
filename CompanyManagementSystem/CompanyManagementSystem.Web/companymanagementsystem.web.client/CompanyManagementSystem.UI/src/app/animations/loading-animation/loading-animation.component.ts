import { Component } from '@angular/core';
import { AnimationItem } from 'lottie-web';
import { AnimationOptions, LottieComponent } from 'ngx-lottie';

@Component({
  selector: 'cms-loading-animation',
  standalone: true,
  imports: [LottieComponent],
  templateUrl: './loading-animation.component.html',
  styleUrls: ['./loading-animation.component.scss'],
})
export class LoadingAnimationComponent {
  options: AnimationOptions = {
    path: '/assets/animations/loading.json',
  };

  styles: Partial<CSSStyleDeclaration> = {
    maxWidth: '500px',
    margin: '0 auto',
  };

  animationCreated(animationItem: AnimationItem): void {
    console.log(animationItem);
  }
}
