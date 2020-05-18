import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';;
import { IAcceptedJobDetail } from '../shared/viewmodels/job-detail-accepted';

@Component({
    selector: 'accepted-jobs',
    templateUrl: './accepted-jobs.component.html',
    styleUrls: ['./accepted-jobs.component.scss']
})
export class AcceptedJobsComponent implements OnInit {
    @Input()
    public acceptedJobList: IAcceptedJobDetail[];

    public constructor() {
    
    }

    public ngOnInit(): void {
        
    }
}
