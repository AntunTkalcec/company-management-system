import { Routes } from '@angular/router';
import { AuthLayoutComponent } from './components/layout/auth-layout/auth-layout.component';
import { LoginContainerComponent } from './pages/login-container/login-container.component';

export const routes: Routes = [
    {
        path: 'login',
        component: AuthLayoutComponent,
        children: [
            { path: '', component: LoginContainerComponent }
        ]
    }
];
