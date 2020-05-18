import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IInvitedJobDetail } from '../shared/viewmodels/job-detail-invited';

@Component({
    selector: 'invited-jobs',
    templateUrl: './invited-jobs.component.html',
    styleUrls: ['./invited-jobs.component.scss']
})
export class InvitedJobsComponent implements OnInit {
    @Input()
    public invitedJobList: IInvitedJobDetail[];

    @Output()
    public onAcceptClick: EventEmitter<any> = new EventEmitter<any>();

    @Output()
    public onDeclineClick: EventEmitter<any> = new EventEmitter<any>();

    public constructor() {
    
    }

    public ngOnInit(): void {
        
    }

    // Method to handle Accept button click
  public onAccept(jobId: number) {
    console.log('onAccept : ', jobId);
        this.onAcceptClick.emit(jobId);
    }

    // Method to handle Decline button click
  public onDecline(jobId: number) {
    console.log('onAccept : ', jobId);
        this.onDeclineClick.emit(jobId);
    }

}
