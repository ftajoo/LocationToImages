import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MyListRoutingModule } from './my-list-routing.module';
import { MyListComponent } from './page/my-list/my-list.component';
import { SharedModule } from '@shared/shared.module';


@NgModule({
  declarations: [
    MyListComponent
  ],
  imports: [
    CommonModule,
    MyListRoutingModule,
    SharedModule
  ]
})
export class MyListModule { }
