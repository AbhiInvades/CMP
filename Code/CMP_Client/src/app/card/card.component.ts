import { Component, Input, OnInit } from '@angular/core';
import { Clinic } from 'src/Models/Clinic';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {

  @Input()  clinic !: Clinic

  constructor() { }

  ngOnInit(): void {
  }

}
