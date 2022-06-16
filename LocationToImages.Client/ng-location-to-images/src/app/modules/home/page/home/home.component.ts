import { animate, query, stagger, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GeoLocation } from '@data/schema/geo-location.model';
import { Location } from '@data/schema/location.model';
import { Photo } from '@data/schema/photo.model';
import { Response } from '@data/schema/response.model';
import { LocationService } from '@data/service/location.service';
import { PhotoService } from '@data/service/photo.service';
import { UserService } from '@data/service/user.service';

interface SearchType {
  type: string;
  label: string;
  tooltip: string;
}

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
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  animations: [fadeAnimation, listAnimation]
})
export class HomeComponent implements OnInit {
  error!: string;
  isLoading: boolean = false;
  formSearch!: FormGroup;
  photos: Photo[] = [];
  photosFiltered: Photo[] = [];
  searchTypes: SearchType[] = [];
  selectedSearchType!: SearchType;
  locations: Location[] = [];
  isLoadingLocations: boolean = false;
  pageSize: number = 10;
  isLoadingSavePhotos: boolean = false;
  isLoadingAddLocationToList: boolean = false;
  currentGeoLocation!: GeoLocation | null;

  get f() {
    return this.formSearch.controls;
  }

  constructor(private _photoService: PhotoService,
    private _locationService: LocationService,
    private _userService: UserService,
    private _formBuilder: FormBuilder,
    private _matSnackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.searchTypes = [
      {
        type: 'from-saved-locations',
        label: 'From saved locations',
        tooltip: 'Search images online from a list of saved locations'
      },
      {
        type: 'online-search',
        label: 'Online search',
        tooltip: 'Search images online by typing new location'
      }
    ];

    this.selectedSearchType = this.searchTypes[0];

    this.buildForm();

    this.f['searchType'].valueChanges.subscribe(val => {
      this.selectedSearchType = val;

      if (val === this.searchTypes[0].type) {
        this.f['search'].clearValidators();
        this.f['search'].updateValueAndValidity();

        this.f['searchFrom'].setValidators([Validators.required]);
        this.f['searchFrom'].updateValueAndValidity();
      } else {
        this.f['searchFrom'].clearValidators();
        this.f['searchFrom'].updateValueAndValidity();

        this.f['search'].setValidators([Validators.required]);
        this.f['search'].updateValueAndValidity();
      }

      this.photos = [];
      this.photosFiltered = [];
    });

    this.isLoadingLocations = true;
    this.f['searchFrom'].disable();
    this._locationService.getLocations()
      .subscribe({
        next: (locationsResponse: Response<Location[]>) => {
          this.isLoadingLocations = false;
          this.f['searchFrom'].enable();

          if (locationsResponse) {
            this.locations = locationsResponse.Data;
          }
        },
        error: (err: Error) => {
          this._matSnackBar.open(err.message, 'Ok');
          this.isLoadingLocations = false;
          this.f['searchFrom'].enable();
        }
      });
  }

  onSubmit(): void {
    this.isLoading = true;
    this.photos = [];
    this.photosFiltered = [];

    if (!this.formSearch.valid) {
      this.isLoading = false;
      return;
    }

    let search: string;

    if (this.selectedSearchType.type === this.searchTypes[0].type) {
      search = this.locations.find(l => l.Id === this.f['searchFrom'].value)?.Address ?? '';
    } else {
      search = this.f['search'].value;
    }

    this.enableControls(false);

    this._locationService
      .getLocation(search)
      .subscribe({
        next: (geoLocationResponse: Response<GeoLocation>) => {
          this.currentGeoLocation = geoLocationResponse.Data;
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
        },
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

  savePhotos(): void {
    this.isLoadingSavePhotos = true;
    this.enableControls(false);

    this._photoService
      .save(this.photos)
      .subscribe({
        next: (photosResponse: Response<Photo[]>) => {
          let message: string = `${photosResponse.Data.length} photos saved successfully. ${photosResponse.Data.length !== this.photos.length ? 'Some photos were already saved.' : ''}`;
          this._matSnackBar.open(message, 'Ok', {duration: 5000});
          this.isLoadingSavePhotos = false;
          this.enableControls(true);
        },
        error: (err: Error) => {
          this._matSnackBar.open(err.message, 'Ok');
          this.isLoadingSavePhotos = false;
          this.enableControls(true);
        }
      });
  }

  addLocationToList(): void {
    if (!this.currentGeoLocation) {
      return;
    }

    this.isLoadingAddLocationToList = true;
    this.enableControls(false);

    this._userService
      .addGeoLocation(this.currentGeoLocation)
      .subscribe({
        next: (geoLocationResponse: Response<GeoLocation>) => {
          this._matSnackBar.open('Location added to list successfully', 'Ok', {duration: 5000});
          this.isLoadingAddLocationToList = false;
          this.enableControls(true);
        },
        error: (err: Error) => {
          this._matSnackBar.open(err.message, 'Ok');
          this.isLoadingAddLocationToList = false;
          this.enableControls(true);
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
        let endIndex: number = this.photos.length < this.pageSize ? this.photos.length : this.pageSize;
        this.photosFiltered = this.photos.slice(0, endIndex);
      }
    }
  }

  private buildForm(): void {
    this.formSearch = this._formBuilder.group({
      searchType: [this.selectedSearchType, Validators.required],
      searchFrom: [''],
      search: ['']
    });
  }

  private enableControls(enable: boolean): void {
    if (enable) {
      this.f['search'].enable();
      this.f['searchFrom'].enable();
      return;
    }

    this.f['search'].disable();
    this.f['searchFrom'].disable();
  }
}
