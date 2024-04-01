import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'cms-nav-bar',
  standalone: true,
  imports: [RouterLink, NgbModule],
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
  @Output() toggleSidebar: EventEmitter<void> = new EventEmitter<void>();

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {}

  logout() {
    this.authService.logoutUser();
  }

  onToggleSidebar() {
    this.toggleSidebar.emit();
  }
}
