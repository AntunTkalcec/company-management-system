import { Component, OnInit } from '@angular/core';
import { LoginModalComponent } from '../../components/layout/modals/login-modal/login-modal.component';

@Component({
  selector: 'app-login-container',
  templateUrl: './login-container.component.html',
  styleUrls: ['./login-container.component.css'],
  standalone: true,
  imports: [LoginModalComponent]
})
export class LoginContainerComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
