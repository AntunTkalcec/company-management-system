import { Component, OnInit } from '@angular/core';
import { Company } from '../../interfaces/company';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'cms-company',
  standalone: true,
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.scss'],
})
export class CompanyComponent implements OnInit {
  company!: Company;
  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.route.paramMap.subscribe((params) => {
      const companyId = params.get('id');
      if (companyId) {
        this.company.name = companyId;
      }
    });
  }
}
