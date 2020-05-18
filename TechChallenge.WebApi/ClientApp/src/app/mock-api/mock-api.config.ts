// App level configuration details
export const AppConfig = {
  apiBaseUrl: 'https://localhost:44392/api'
}

import { HttpResponse } from '@angular/common/http';
import { of } from 'rxjs';
export default {
  GET: {
    'https://localhost:44392/api/TradieLead/invited': {
      handler: getInvitedJobs
    },

    'https://localhost:44392/api/TradieLead/accepted': {
      handler: getAcceptedJobs
    },
  },
  PATCH: {
    'https://localhost:44392/api/TradieLead/accept/${id}': {
      handler: acceptInvitation
    },

    'https://localhost:44392/apiTradieLead/decline/${id}': {
      handler: declineInvitation
    }
  }
}

function getInvitedJobs() {
  return of(new HttpResponse({
    status: 200, body: [{
      id: 1,
      contactName: 'Bob',
      createdDateTime: '2020-05-18T12:46:19',
      suburb: 'Woodcroft',
      categoryName: 'Painters',
      description: 'Need to piant 2 aluminium windows and a sliding glass door',
      price: 70
    },
    {
      id: 2,
      contactName: 'Andrew',
      createdDateTime: '2020-05-18T10:46:19',
      suburb: 'Blacktown',
      categoryName: 'Carpentery',
      description: 'Need to assemble doors and windows',
      price: 330
    },
    {
      id: 3,
      contactName: 'Graeme',
      createdDateTime: '2020-06-18T12:46:19',
      suburb: 'Sydney',
      categoryName: 'Carpentery',
      description: 'Need to assemble doors and windows',
      price: 330
    },
    {
      id: 4,
      contactName: 'Steve',
      createdDateTime: '2020-05-20T12:46:19',
      suburb: 'Sydney',
      categoryName: 'Carpentery',
      description: 'Need to assemble doors and windows',
      price: 330
    }]
  }))
}

function getAcceptedJobs() {
  return of(new HttpResponse({
    status: 200, body: [{
      id: 1,
      contactName: 'Bob',
      createdDateTime: '2020-05-18T12:46:19',
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
      createdDateTime: '2020-05-18T10:46:19',
      suburb: 'Blacktown',
      postcode: '2501',
      categoryName: 'Carpentery',
      description: 'Need to assemble doors and windows',
      price: 330,
      contactNumber: '0481444524',
      contactEmail: 'testing@gmail.com'
     }]
  }))
}

function acceptInvitation(id) {
  return of(new HttpResponse({
    status: 200, body: {
      jobId: id,
      Status: 'Accepted'
    }
  }))
}

function declineInvitation(id) {
  return of(new HttpResponse({
    status: 200, body: {
      jobId: id,
      Status: 'Declined'
    }
  }))
}
