import { TestBed, inject, fakeAsync, tick } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { JobService } from './job.service';

describe('JobService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [JobService]
    });
  });

  it('should be initialized',
    inject([JobService], (jobService: JobService) => {
      expect(jobService).toBeTruthy();
    })
  );

  it('should return accepted jobs list',
    fakeAsync(
      inject([JobService, HttpTestingController],
        (jobService: JobService, backend: HttpTestingController) => {

          // Set up
          const reqUrl = 'https://localhost:44392/api/TradieLead/accepted';
          const responseObject = [{
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

          let response = null;
          // End Setup

          jobService.getAcceptedJobs().subscribe(
            (receivedResponse: any) => {
              response = receivedResponse;
            },
            (error: any) => { }
          );

          const requestWrapper = backend.expectOne({ url: reqUrl });
          requestWrapper.flush(responseObject);

          tick();

          expect(requestWrapper.request.method).toEqual('GET');
          expect(response).toEqual(responseObject);
          expect(response.length).toEqual(2);
        }
      )
    )
  );

  it('should return invited jobs list',
    fakeAsync(
      inject([JobService, HttpTestingController],
        (jobService: JobService, backend: HttpTestingController) => {

          // Set up
          const reqUrl = 'https://localhost:44392/api/TradieLead/invited';
          const responseObject = [{
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

          let response = null;
          // End Setup

          jobService.getInvitedJobs().subscribe(
            (receivedResponse: any) => {
              response = receivedResponse;
            },
            (error: any) => { }
          );

          const requestWrapper = backend.expectOne({ url: reqUrl });
          requestWrapper.flush(responseObject);

          tick();

          expect(requestWrapper.request.method).toEqual('GET');
          expect(response).toEqual(responseObject);
          expect(response.length).toEqual(4);
        }
      )
    )
  );

  it('should accept job',
    fakeAsync(
      inject([JobService, HttpTestingController],
        (jobService: JobService, backend: HttpTestingController) => {

          // Set up
          const reqUrl = 'https://localhost:44392/api/TradieLead/accept/1';
          const responseObject = {
            jobId: 1,
            Status: 'Accepted'
          };

          let response = null;
          // End Setup

          jobService.acceptInvitation(1).subscribe(
            (receivedResponse: any) => {
              response = receivedResponse;
            },
            (error: any) => { }
          );

          const requestWrapper = backend.expectOne({ url: reqUrl });
          requestWrapper.flush(responseObject);

          tick();

          expect(requestWrapper.request.method).toEqual('PATCH');
          expect(response).toEqual(responseObject);
          expect(response.jobId).toEqual(1);
          expect(response.Status).toEqual('Accepted');
        }
      )
    )
  );

  it('should decline job',
    fakeAsync(
      inject([JobService, HttpTestingController],
        (jobService: JobService, backend: HttpTestingController) => {

          // Set up
          const reqUrl = 'https://localhost:44392/api/TradieLead/decline/1';
          const responseObject = {
            jobId: 1,
            Status: 'Declined'
          };

          let response = null;
          // End Setup

          jobService.declineInvitation(1).subscribe(
            (receivedResponse: any) => {
              response = receivedResponse;
            },
            (error: any) => { }
          );

          const requestWrapper = backend.expectOne({ url: reqUrl });
          requestWrapper.flush(responseObject);

          tick();

          expect(requestWrapper.request.method).toEqual('PATCH');
          expect(response).toEqual(responseObject);
          expect(response.jobId).toEqual(1);
          expect(response.Status).toEqual('Declined');
        }
      )
    )
  );
})



//private mockedInvitedJobList(): IInvitedJobDetail[] {
//  return

//}

//    private mockedAcceptedJobList(): IAcceptedJobDetail[] {
//  return [{
//    id: 1,
//    contactName: 'Bob',
//    createdDateTime: 'January 4 @ 02:37 pm',
//    suburb: 'Woodcroft',
//    postcode: '2767',
//    categoryName: 'Painters',
//    description: 'Need to piant 2 aluminium windows and a sliding glass door',
//    price: 70,
//    contactNumber: '0481254524',
//    contactEmail: 'test@gmail.com'
//  },
//  {
//    id: 2,
//    contactName: 'Andrew',
//    createdDateTime: 'February 4 @ 02:37 pm',
//    suburb: 'Blacktown',
//    postcode: '2501',
//    categoryName: 'Carpentery',
//    description: 'Need to assemble doors and windows',
//    price: 330,
//    contactNumber: '0481444524',
//    contactEmail: 'testing@gmail.com'
//  }];
//}
