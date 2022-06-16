import { Component, Input, OnInit } from '@angular/core';
import { Photo } from '@data/schema/photo.model';

@Component({
  selector: 'app-photo-card',
  templateUrl: './photo-card.component.html',
  styleUrls: ['./photo-card.component.css']
})
export class PhotoCardComponent implements OnInit {
  @Input() photo!: Photo;

  constructor() { }

  ngOnInit(): void {
  }

}
