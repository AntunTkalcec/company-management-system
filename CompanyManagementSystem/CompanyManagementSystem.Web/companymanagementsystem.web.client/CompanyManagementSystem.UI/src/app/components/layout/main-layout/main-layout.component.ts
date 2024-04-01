import { Component, OnInit } from '@angular/core';
import { NavBarComponent } from '../../../other/nav-bar/nav-bar.component';
import { RouterOutlet } from '@angular/router';
import { SideNavComponent } from '../../../other/side-nav/side-nav.component';

@Component({
  selector: 'cms-main-layout',
  standalone: true,
  imports: [NavBarComponent, RouterOutlet, SideNavComponent],
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss'],
})
export class MainLayoutComponent implements OnInit {
  isSidebarOpen = false;

  constructor() {}

  ngOnInit() {}

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }
}
