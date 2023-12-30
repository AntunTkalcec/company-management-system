import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login-modal',
  standalone: true,
  imports: [],
  templateUrl: './login-modal.component.html',
  styleUrl: './login-modal.component.css'
})
export class LoginModalComponent {
  @Input() errorMsg!: string;
  @Output() formEmitter: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
  @Output() clearMsg: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(private fb: FormBuilder) { }

  loginForm: FormGroup = this.fb.group({
    emailOrUsername: ['', Validators.required],
    password: ['', Validators.required]
  });

  onSubmit(): void {

  }
}