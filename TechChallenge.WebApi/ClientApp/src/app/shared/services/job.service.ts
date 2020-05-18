import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { of } from 'rxjs';
import { IInvitedJobDetail } from '../viewmodels/job-detail-invited';
import { IAcceptedJobDetail } from '../viewmodels/job-detail-accepted';
import { HttpClient } from '@angular/common/http';
import { AppConfig } from '../../app.config';

@Injectable()
export class JobService {
    apiBaseUrl: string = AppConfig.apiBaseUrl;
    constructor(private http: HttpClient) { }
    
    // Service method to retrieve Invited job list
    public getInvitedJobs(): Observable<IInvitedJobDetail[]> {
        // return of(this.mockedInvitedJobList());
        return this.http.get<IInvitedJobDetail[]>(this.apiBaseUrl + '/TradieLead/invited');
    }

    // Service method to retrieve Invited job list
    public getAcceptedJobs(): Observable<IAcceptedJobDetail[]> {
      // return of(this.mockedAcceptedJobList());
      return this.http.get<IAcceptedJobDetail[]>(this.apiBaseUrl + '/TradieLead/accepted');
    }

    // Service method to Accept invitation
    public acceptInvitation(id: number): Observable<any> {
      console.log(this.apiBaseUrl + `TradieLead/accept/${id}`);
      return this.http.patch(this.apiBaseUrl + `/TradieLead/accept/${id}`, {});
    }

    // Service method to Decline invitation
  public declineInvitation(id: number): Observable<any> {
    console.log(this.apiBaseUrl + `TradieLead/accept/${id}`);
    return this.http.patch(this.apiBaseUrl + `/TradieLead/decline/${id}`, {});
  }

    private mockedInvitedJobList(): IInvitedJobDetail[] {
        return [{
            id: 1,
            contactName: 'Bob',
            createdDateTime: 'January 4 @ 02:37 pm',
            suburb: 'Woodcroft',
            categoryName: 'Painters',
            description: 'Need to piant 2 aluminium windows and a sliding glass door',
            price: 70
        },
        {
            id: 2,
            contactName: 'Andrew',
            createdDateTime: 'February 4 @ 02:37 pm',
            suburb: 'Blacktown',
            categoryName: 'Carpentery',
            description: 'Need to assemble doors and windows',
            price: 330
        },
        {
            id: 3,
            contactName: 'Graeme',
            createdDateTime: 'March 4 @ 02:37 pm',
            suburb: 'Sydney',
            categoryName: 'Carpentery',
            description: 'Need to assemble doors and windows',
            price: 330
        },
        {
            id: 4,
            contactName: 'Steve',
            createdDateTime: 'March 4 @ 02:37 pm',
            suburb: 'Sydney',
            categoryName: 'Carpentery',
            description: 'Need to assemble doors and windows',
            price: 330
        }];
    }

    private mockedAcceptedJobList(): IAcceptedJobDetail[] {
        return [{
            id: 1,
            contactName: 'Bob',
            createdDateTime: 'January 4 @ 02:37 pm',
            suburb: 'Woodcroft',
            postcode: '2767',
            categoryName: 'Painters',
            description: 'Need to piant 2 aluminium windows and a sliding glass door',
            price: 70,
            contactNumber: '0481254524',
            contactEmail: 'test@gmail.com'
        },
        {
            id: 2,
            contactName: 'Andrew',
            createdDateTime: 'February 4 @ 02:37 pm',
            suburb: 'Blacktown',
            postcode: '2501',
            categoryName: 'Carpentery',
            description: 'Need to assemble doors and windows',
            price: 330,
            contactNumber: '0481444524',
            contactEmail: 'testing@gmail.com'
        }];
    }
}
