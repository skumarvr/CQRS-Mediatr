import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { JobService } from '../shared/services/job.service';
import { forkJoin } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { IInvitedJobDetail } from '../shared/viewmodels/job-detail-invited';
import { IAcceptedJobDetail } from '../shared/viewmodels/job-detail-accepted';
import { MsgBusService } from '../shared/services/msgBus.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']  
})
export class DashboardComponent implements OnInit {
  public isLoadingData: boolean = false;
  public isServerConnectionFailed: boolean = false;
  public invitedJobList: IInvitedJobDetail[] = [];
  public acceptedJobList: IAcceptedJobDetail[] = [];

  public constructor(
    public dialog: MatDialog,
    public jobService: JobService,
    private spinner: NgxSpinnerService,
    private msgBus: MsgBusService
  ) {

  }

  // Initialise Component
  public ngOnInit(): void {
    // Show spinner
    this.showHideSpinner(true);

    // Call Init helper method
    this.onInitHelper();
  }

  private PublishMessage(msg:string) {
    this.msgBus.changeMessage(msg)
  }

  // load Invited Jobs
  private loadInvitedJobs() {
    const observable = this.jobService.getInvitedJobs();
    this.isServerConnectionFailed = false;
    observable.subscribe((results) => {
      this.invitedJobList = results as IInvitedJobDetail[];
      this.PublishMessage('');
    }, (error) => {
        this.PublishMessage("Failed to connect to api server");
    })
  }

  // load Accepted Jobs
  private loadAcceptedJobs() {
    const observable = this.jobService.getAcceptedJobs();
    observable.subscribe((results) => {
      this.acceptedJobList = results as IAcceptedJobDetail[];
      this.PublishMessage('');
    }, (error) => {
      this.PublishMessage("Failed to connect to api server");
    })
  }

  // Handle Tab change event
  public tabChanged(event) {
    if (event == null) {
      return;
    }
      
    // Handle Invited
    if (event.index == 0) {
      this.loadInvitedJobs();
    }

    // Handle Accpeted
    if (event.index == 1) {
      this.loadAcceptedJobs();
    }
  }

  // Method to handle Accept Invitation 
  public acceptInvitation(id: number) {
    if (!id) {
      return;
    }

    // Show spinner
    this.showHideSpinner(true);
      
    // Call service method to change job status to accepted
    this.jobService.acceptInvitation(id).subscribe((response) => {
      // Refresh Job list
      this.loadInvitedJobs();
      this.PublishMessage('');
      // Hide spinner
      this.showHideSpinner(false);
    }, (error) => {
      // Hide spinner
      this.PublishMessage("Failed to connect to api server");
      this.showHideSpinner(false);
    });
  }

  // Method to handle Decline Invitation 
  public declineInvitation(id: number) { 
    if (id) {
      // Initialise Confirm Dialog
      const dialogRef = this.initialiseConfirmDialog();
  
      // Subscribe for Dialog action
      dialogRef.afterClosed().subscribe(result => {
        // If true decline job
        if (result) {
          // Show spinner
          this.showHideSpinner(true);
  
          // Call service method to change job status to accepted
          this.jobService.declineInvitation(id).subscribe((response) => {
            // Refresh Job list
            this.loadInvitedJobs();
            this.PublishMessage('');
            // Hide spinner
            this.showHideSpinner(false);
          }, (error) => {
            console.log(error);  
            // Hide spinner
            this.PublishMessage("Failed to connect to api server");
            this.showHideSpinner(false);
          }); 
        }
      });
    }
  }

  // Helper method for Initialise
  private onInitHelper(): void {
    // Create calls for Fork join
    const getInvitedJobs = this.jobService.getInvitedJobs();
    const getAcceptedJobs = this.jobService.getAcceptedJobs();

    // Fork join to get all responses
    forkJoin(getInvitedJobs, getAcceptedJobs).subscribe((results) => {
      // Invited Jobs
      this.invitedJobList = results[0] as IInvitedJobDetail[];
      // Accepted Jobs
      this.acceptedJobList = results[1] as IAcceptedJobDetail[];

      this.PublishMessage('');
      // Hide spinner
      this.showHideSpinner(false);
    }, (error) => {
        console.log(error);
        this.PublishMessage('Failed to connect to api server');
        // Hide spinner
        this.showHideSpinner(false);
    });
  }

  // Method to show/hide spinner
  private showHideSpinner(showSpinner: boolean) {
    showSpinner ? this.spinner.show() : this.spinner.hide();
  }

  // Method to initialise Confirm dialog
  private initialiseConfirmDialog(): any {
    return this.dialog.open(ConfirmationDialogComponent, {
      width: '450px',
      height: '160px',
      data: {
        message: 'Do you want to decline this job?'
      }
    });
  }
}
