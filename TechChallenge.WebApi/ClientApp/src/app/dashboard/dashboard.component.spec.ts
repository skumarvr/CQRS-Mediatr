import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardComponent } from './dashboard.component';
import { MatDialog } from '@angular/material/dialog';
import { JobService } from '../shared/services/job.service';
import { MsgBusService } from '../shared/services/msgBus.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


describe('DashboardComponent', () => {
  let component: DashboardComponent;
  let fixture: ComponentFixture<DashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule
      ],
      declarations: [DashboardComponent],
      providers: [
        { provide: MatDialog, useValue: {} },
        {
          provide: MsgBusService, useValue: {
            changeMessage: (message) => ({})
          }
        },
        {
          provide: JobService,
          useValue: {
            getInvitedJobs: () => ([{
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
              }]),

            getAcceptedJobs: () => ([{
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
              }]),

            acceptInvitation: (id) => ({
                jobId: 1,
                Status: 'Accepted'
              }),

            declineInvitation: (id) => ({
                jobId: 1,
                Status: 'Declined'
              })
          }
        }
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should get the accepted leads', () => {
    const getAcceptedJobsSpy = spyOn(
      component.jobService,
      'getAcceptedJobs'
    );

    component.ngOnInit();

    expect(getAcceptedJobsSpy).toHaveBeenCalled();
  });

  it('should get the invited leads', () => {
    const getInvitedJobsSpy = spyOn(
      component.jobService,
      'getInvitedJobs'
    );

    component.ngOnInit();

    expect(getInvitedJobsSpy).toHaveBeenCalled();
  });

});
