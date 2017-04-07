import { Component, OnInit } from '@angular/core';
import {MessagesService,MessageDeliveryStatus,SourceSystem,OperationType} from '../services/messages-service'
import {Observable} from 'rxjs/Observable'
@Component({
  moduleId: module.id.toString(),
  selector: 'app-messages-viewer',
  templateUrl: './messages-viewer.component.html',
  styleUrls: ['./messages-viewer.component.css']
})
export class MessagesViewerComponent implements OnInit {


  messages: Array<MessageDeliveryStatus>=[]
  showDetails: Array<boolean>=[];
  subscriptions: Array<string>=[];
  title: string = "Title";
  showJsonEditor: boolean = false;
  sendingMessages = false;
  messagesReceived = false;
  constructor(private _messagesService:MessagesService ) { }

  ngOnInit() {
    var that= this;
    this._messagesService.getMessages().subscribe(c=> {
      that.messages=c
      that.messages.forEach(x=> {
        if (!x.defectedMessage)
        {
          x.data["sourceSystemName"] = SourceSystem[x.data.sourceSystem];
          x.data["operationTypeName"] = OperationType[x.data.operationType];
        }
      })
      that.showDetails = that.messages.map(c => false); 
      that.messagesReceived = true;
    });
    this._messagesService.getSubscriptions().subscribe(subs=>that.subscriptions=subs);
  }

  isEmpty() {
    
  }

  isMessageDelivered(msg:MessageDeliveryStatus,sub:string):string
  {
    if (msg.data == null || msg.data.contactRecord == null)
      return "Failure";
    if (typeof(msg.failedSubscriptions.find((value)=>value == sub)) !== "undefined")
      return "Success"
    else
      return "Failure"
  }
  closeJsonEditor()
  {
    this.showJsonEditor=false;
  }



  sendMessageJsonEditor()
  {
    this.sendingMessages = !this.sendingMessages;
  }
}

