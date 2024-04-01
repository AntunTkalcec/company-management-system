import { Component, OnInit } from '@angular/core';
import { LoadingAnimationComponent } from '../../animations/loading-animation/loading-animation.component';
import { DashboardService } from '../../services/dashboard.service';
import { Company } from '../../interfaces/company';
import { Router } from '@angular/router';

@Component({
  selector: 'cms-home',
  standalone: true,
  imports: [LoadingAnimationComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  public company!: Company;

  constructor(
    private dashboardService: DashboardService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.dashboardService.getCompanyInfo().subscribe((c) => {
      this.company = c;

      if (this.company && this.company.companyImage) {
        this.convertImageToBase64();
      }
    });
  }

  convertImageToBase64(): void {
    const byteCharacters = atob(this.company!.companyImage); // Decode the byte array
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: 'image/jpeg' });
    const reader = new FileReader();
    reader.readAsDataURL(blob);
    reader.onloadend = () => {
      this.company!.companyImage = reader.result as string;
    };
  }

  navigateToCompanyDetails() {
    this.router.navigate(['/company', this.company.name], {
      queryParams: { id: this.company.id.toString() },
    });
  }
}
