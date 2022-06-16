import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SavedPhotosComponent } from './page/saved-photos/saved-photos.component';

export const routes: Routes = [
  {
    path: '',
    component: SavedPhotosComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SavedPhotosRoutingModule { }
