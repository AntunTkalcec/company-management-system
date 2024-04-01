import { NgClass } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'cms-side-nav',
  standalone: true,
  imports: [NgClass, RouterLink, RouterLinkActive],
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss'],
})
export class SideNavComponent implements OnInit {
  @Input() isSidebarOpen = false;
  constructor() {}

  ngOnInit() {}

  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }
}
