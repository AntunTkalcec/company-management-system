import { Component, inject, OnInit } from '@angular/core';
import { ToastService } from '../../services/toast.service';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'cms-toast-container',
  standalone: true,
  imports: [NgbToastModule],
  templateUrl: './toast-container.component.html',
  styleUrls: ['./toast-container.component.scss'],
  // eslint-disable-next-line @angular-eslint/no-host-metadata-property
  host: {
    class: 'toast-container position-fixed top-0 end-0 p-3',
    style: 'z-index: 1200',
  },
})
export class ToastContainerComponent implements OnInit {
  toastService = inject(ToastService);
  constructor() {}

  ngOnInit() {}
}
