import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { NgxSpinnerModule } from 'ngx-spinner';
import { MatDialogModule } from '@angular/material/dialog';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DashboardComponent } from './dashboard/dashboard.component';
import { JobService } from './shared/services/job.service';
import { HttpClientModule } from '@angular/common/http';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { InvitedJobsComponent } from './invited-jobs/invited-jobs.component';
import { AcceptedJobsComponent } from './accepted-jobs/accepted-jobs.component';
import { FirstNamePipe } from './shared/pipes/first-name.pipe';
import { EmptyJobComponent } from './empty-job/empty-job.component';
import { MsgBusService } from './shared/services/msgBus.service';
import { environment } from '../environments/environment';
import { MockApiModule } from './mock-api/mock-api.module';

let extraModules = environment.mockApi ? [MockApiModule] : [];

@NgModule({
  declarations: [
    AppComponent,
    InvitedJobsComponent,
    AcceptedJobsComponent,
    DashboardComponent,
    ConfirmationDialogComponent,
    FirstNamePipe,
    EmptyJobComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatTabsModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    HttpClientModule,
    NgxSpinnerModule,
    MatDialogModule,
    extraModules
  ],
  exports: [
    
  ],
  providers: [
    JobService,
    MsgBusService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
