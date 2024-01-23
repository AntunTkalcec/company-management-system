import { Routes } from '@angular/router';
import { AuthLayoutComponent } from './components/layout/auth-layout/auth-layout.component';
import { LoginContainerComponent } from './pages/login-container/login-container.component';
import { HomeComponent } from './pages/home/home.component';
import { MainLayoutComponent } from './components/layout/main-layout/main-layout.component';
import { AuthGuard } from './app.guard';

export const routes: Routes = [
    {
        path: 'login',
        component: AuthLayoutComponent,
        children: [
            { path: '', component: LoginContainerComponent }
        ]
    },
    {
        path: '',
        component: MainLayoutComponent,
        canActivate: [AuthGuard],
        children: [
            { path: '', component: HomeComponent }
        ]
    }
];
