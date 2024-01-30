import { Component, inject } from '@angular/core';
import { MessageService } from '../message.service';
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-messages',
  standalone: true,
  templateUrl: './messages.component.html',
  styleUrl: './messages.component.css',
  imports: [ NgFor, NgIf ]
})
export class MessagesComponent {
  messageService = inject(MessageService)
}
