import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SavedPhotosRoutingModule } from './saved-photos-routing.module';
import { SavedPhotosComponent } from './page/saved-photos/saved-photos.component';
import { SharedModule } from '@shared/shared.module';


@NgModule({
  declarations: [
    SavedPhotosComponent
  ],
  imports: [
    CommonModule,
    SavedPhotosRoutingModule,
    SharedModule
  ]
})
export class SavedPhotosModule { }
