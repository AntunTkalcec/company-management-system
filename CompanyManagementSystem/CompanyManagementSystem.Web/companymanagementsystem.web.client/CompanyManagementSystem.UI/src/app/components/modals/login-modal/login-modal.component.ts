import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'cms-login-modal',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login-modal.component.html',
  styleUrls: ['./login-modal.component.scss'],
})
export class LoginModalComponent {
  @Input() errorMsg!: string;
  @Output() loginFormEmitter: EventEmitter<FormGroup> =
    new EventEmitter<FormGroup>();
  @Output() registerFormEmitter: EventEmitter<FormGroup> =
    new EventEmitter<FormGroup>();
  @Output() clearMsg: EventEmitter<boolean> = new EventEmitter<boolean>();

  @Input() loggingIn: boolean = false;
  @Input() logIn: boolean = true;
  @Input() registering: boolean = false;

  loginForm: FormGroup = this.fb.group({
    emailOrUsername: ['', Validators.required],
    password: ['', Validators.required],
  });

  registerForm: FormGroup = this.fb.group({
    email: [
      '',
      [
        Validators.required,
        Validators.pattern(
          "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"
        ),
      ],
    ],
    username: ['', Validators.required],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    password: ['', Validators.required],
  });

  constructor(private fb: FormBuilder) {}

  onLoginSubmit(): void {
    this.loginFormEmitter.emit(this.loginForm);
  }

  onLogin(): void {
    this.logIn = true;
  }

  onRegister(): void {
    this.logIn = false;
  }

  onRegisterSubmit(): void {
    this.registerFormEmitter.emit(this.registerForm);
  }

  onType(): void {
    if (this.errorMsg !== undefined) {
      this.clearMsg.emit(true);
    }
  }
}
