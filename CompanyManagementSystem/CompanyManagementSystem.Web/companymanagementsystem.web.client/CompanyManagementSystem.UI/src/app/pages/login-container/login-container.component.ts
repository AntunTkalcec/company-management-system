import { Component, OnInit } from '@angular/core';
import { LoginModalComponent } from '../../components/modals/login-modal/login-modal.component';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { HttpResponse } from '@angular/common/http';
import { ToastService } from '../../services/toast.service';
import { StorageService } from '../../services/storage.service';

@Component({
  selector: 'cms-login-container',
  standalone: true,
  templateUrl: './login-container.component.html',
  imports: [LoginModalComponent],
  styleUrls: ['./login-container.component.scss'],
})
export class LoginContainerComponent implements OnInit {
  errorMsg!: string;
  logIn: boolean = true;
  loggingIn!: boolean;
  registering!: boolean;

  constructor(
    private authService: AuthService,
    private router: Router,
    private toastService: ToastService,
    private storage: StorageService
  ) {}

  ngOnInit() {
    if (this.storage.get('authData')) {
      this.router.navigate(['']);
    }
  }

  clearMsg(shouldClear: boolean) {
    if (shouldClear) {
      this.errorMsg = '';
    }
  }

  loginUser(formData: FormGroup) {
    this.loggingIn = true;
    this.errorMsg = '';

    this.authService.loginUser(formData.value).subscribe({
      next: (response: HttpResponse<any>) => {
        if (response.status == 200) {
          this.registering = false;
          this.loggingIn = false;
          this.logIn = true;
          this.router.navigate(['']);
        }
      },
      error: (response: HttpResponse<any>) => {
        this.registering = false;
        this.loggingIn = false;
        this.logIn = true;
        if (response.status == 404) {
          this.errorMsg =
            'Incorrect email/username or password. Please try again';
        } else if (response.status == 500) {
          this.errorMsg = 'Internal server error';
        } else {
          this.errorMsg = 'Unknown error';
        }
      },
    });
  }

  registerUser(formData: FormGroup) {
    this.registering = true;
    this.errorMsg = '';

    this.authService.registerUser(formData.value).subscribe({
      next: (response: HttpResponse<any>) => {
        if (response.status == 201) {
          this.toastService.show({
            body: 'Registration successful!',
            classname: 'bg-success text-light',
          });
          this.registering = false;
          this.loggingIn = false;
          this.logIn = true;
        }
      },
      error: (response: HttpResponse<any>) => {
        this.toastService.show({
          body: 'Registration failed',
          classname: 'bg-danger text-light',
        });

        this.registering = false;
        this.loggingIn = false;
        if (response.status == 400) {
          this.errorMsg = 'An incorrect input may have occurred.';
        } else if (response.status == 500) {
          this.errorMsg = 'Internal server error';
        } else {
          this.errorMsg = 'Unknown error';
        }
      },
    });
  }
}
