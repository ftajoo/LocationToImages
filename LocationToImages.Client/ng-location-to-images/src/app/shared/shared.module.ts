import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MaterialModule } from './material.module';
import { FlexLayoutModule } from '@angular/flex-layout';

import { ControlMessagesComponent } from './component/control-messages/control-messages.component';
import { SpinnerComponent } from './component/spinner/spinner.component';
import { PhotoCardComponent } from './component/photo-card/photo-card.component';

@NgModule({
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterModule, FlexLayoutModule, MaterialModule],
  declarations: [ControlMessagesComponent, SpinnerComponent, PhotoCardComponent],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,

    MaterialModule,

    ControlMessagesComponent,
    SpinnerComponent,

    FlexLayoutModule,
    PhotoCardComponent
  ],
})
export class SharedModule {}
