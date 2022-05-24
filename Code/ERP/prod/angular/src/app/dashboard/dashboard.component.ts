import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  template: `
    <app-host-dashboard *abpPermission="'ERP.Dashboard.Host'"></app-host-dashboard>
    <app-tenant-dashboard *abpPermission="'ERP.Dashboard.Tenant'"></app-tenant-dashboard>
  `,
})
export class DashboardComponent {}
