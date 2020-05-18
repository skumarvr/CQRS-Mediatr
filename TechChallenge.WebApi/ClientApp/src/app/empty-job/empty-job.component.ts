import { Component, OnInit } from '@angular/core';
import { MsgBusService } from '../shared/services/msgBus.service';


@Component({
  selector: 'empty-job',
  templateUrl: './empty-job.component.html',
  styleUrls: ['./empty-job.component.scss']
})
export class EmptyJobComponent implements OnInit {
  defaultMessage: string = 'No Leads to Display';
  message: string;

  public constructor(private msgBus: MsgBusService) {
  }

  public ngOnInit(): void {
    this.msgBus.currentMessage.subscribe(message => this.message = message)   
  }

}
