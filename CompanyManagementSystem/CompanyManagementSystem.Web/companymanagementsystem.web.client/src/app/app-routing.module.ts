import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthLayoutComponent } from "./components/layout/auth-layout/auth-layout.component";
import { LoginContainerComponent } from "./pages/login-container/login-container.component";

const routes: Routes = [
    {
        path: 'login',
        component: AuthLayoutComponent,
        children: [
            { path: '', component: LoginContainerComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }