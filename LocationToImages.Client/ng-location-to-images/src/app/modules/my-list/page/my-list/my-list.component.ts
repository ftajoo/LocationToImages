import { animate, query, stagger, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GeoLocation } from '@data/schema/geo-location.model';
import { Photo } from '@data/schema/photo.model';
import { Response } from '@data/schema/response.model';
import { LocationService } from '@data/service/location.service';
import { PhotoService } from '@data/service/photo.service';
import { UserService } from '@data/service/user.service';

export const fadeAnimation = trigger('fadeAnimation', [
  transition(':enter', [
    style({ opacity: 0 }), animate('300ms', style({ opacity: 1 }))]
  ),
  transition(':leave',
    [style({ opacity: 1 }), animate('300ms', style({ opacity: 0 }))]
  )
]);

const listAnimation = trigger('listAnimation', [
  transition('* <=> *', [
    query(':enter',
      [style({ opacity: 0 }), stagger('60ms', animate('600ms ease-out', style({ opacity: 1 })))],
      { optional: true }
    ),
    query(':leave',
      animate('200ms', style({ opacity: 0 })),
      { optional: true }
    )
  ])
]);

@Component({
  selector: 'app-my-list',
  templateUrl: './my-list.component.html',
  styleUrls: ['./my-list.component.css'],
  animations: [fadeAnimation, listAnimation]
})
export class MyListComponent implements OnInit {
  error!: string;
  isLoading: boolean = false;
  formSearch!: FormGroup;
  photos: Photo[] = [];
  photosFiltered: Photo[] = [];
  geoLocations: GeoLocation[] = [];
  isLoadingGeoLocations: boolean = false;
  pageSize: number = 10;
  isLoadingRemoveLocation: boolean = false;

  get f() {
    return this.formSearch.controls;
  }

  constructor(private _photoService: PhotoService,
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _matSnackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.buildForm();

    this.isLoadingGeoLocations = true;
    this.f['geoLocation'].disable();
    this.loadGeoLocations();
  }

  onSubmit(): void {
    this.isLoading = true;
    this.photos = [];
    this.photosFiltered = [];

    if (!this.formSearch.valid) {
      this.isLoading = false;
      return;
    }

    let search: string = this.geoLocations.find(l => l.Id === this.f['geoLocation'].value)?.Address ?? '';

    this.enableControls(false);

    this._photoService
      .searchByAddress(search)
      .subscribe({
        next: (photosResponse: Response<Photo[]>) => this.handleSearchResponse(photosResponse),
        error: (err: Error) => {
          this._matSnackBar.open(err.message, 'Ok');
          this.isLoading = false;
          this.enableControls(true);
        }
      });
  }

  onPage(event: PageEvent): void {
    let endIndex: number = (event.pageIndex + 1) * event.pageSize;
    endIndex = this.photos.length < endIndex ? this.photos.length : endIndex;
    this.photosFiltered = this.photos.slice(event.pageIndex * event.pageSize, endIndex);
  }

  removeLocation(): void {
    if (!this.f['geoLocation'].value) {
      return;
    }

    let geoLocation: GeoLocation | undefined = this.geoLocations.find(l => l.Id === this.f['geoLocation'].value);

    if (!geoLocation) {
      return;
    }

    this.isLoadingRemoveLocation = true;
    this.enableControls(false);

    this._userService
      .deleteGeoLocation(geoLocation)
      .subscribe({
        next: (response: Response<number>) => {
          this._matSnackBar.open('Location removed successfully.', 'Ok', {duration: 5000});
          this.isLoadingRemoveLocation = false;
          this.enableControls(true);

          this.photos = [];
          this.photosFiltered = [];
          this.loadGeoLocations();
        },
        error: (err: Error) => {
          this._matSnackBar.open(err.message, 'Ok');
          this.isLoadingRemoveLocation = false;
          this.enableControls(true);
        }
      });
  }

  private loadGeoLocations() {
    this._userService.getGeoLocations()
      .subscribe({
        next: (locationsResponse: Response<GeoLocation[]>) => {
          this.isLoadingGeoLocations = false;
          this.f['geoLocation'].enable();

          if (locationsResponse) {
            this.geoLocations = locationsResponse.Data;
          }
        },
        error: (err: Error) => {
          this._matSnackBar.open(err.message, 'Ok');
          this.isLoadingGeoLocations = false;
          this.f['geoLocation'].enable();
        }
      });
  }

  private handleSearchResponse(photosResponse: Response<Photo[]>): void {
    console.log(photosResponse);
    this.isLoading = false;
    this.enableControls(true);

    if (photosResponse) {
      this.photos = photosResponse.Data;

      if (this.photos && this.photos.length > 0) {
        let endIndex: number = this.photos.length < this.pageSize ? this.photos.length : 5;
        this.photosFiltered = this.photos.slice(0, endIndex);
      }
    }
  }

  private buildForm(): void {
    this.formSearch = this._formBuilder.group({
      geoLocation: ['', Validators.required],
    });
  }

  private enableControls(enable: boolean): void {
    if (enable) {
      this.f['geoLocation'].enable();
      return;
    }

    this.f['geoLocation'].disable();
  }
}
