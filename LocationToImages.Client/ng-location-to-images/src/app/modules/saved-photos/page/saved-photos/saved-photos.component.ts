import { animate, query, stagger, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PagedResponse } from '@data/schema/paged-response.model';
import { PaginationFilter } from '@data/schema/pagination-filter.model';
import { PhotoSearchBy, PhotoSearchByMapping } from '@data/schema/photo-search-by.enum';
import { Photo } from '@data/schema/photo.model';
import { Response } from '@data/schema/response.model';
import { PhotoService } from '@data/service/photo.service';

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
  selector: 'app-saved-photos',
  templateUrl: './saved-photos.component.html',
  styleUrls: ['./saved-photos.component.css'],
  animations: [fadeAnimation, listAnimation]
})
export class SavedPhotosComponent implements OnInit {
  error!: string;
  isLoadingPhotos: boolean = false;
  isLoading: boolean = false;
  formSearch!: FormGroup;
  photos: Photo[] = [];
  photoSearchByEnum: typeof PhotoSearchBy = PhotoSearchBy;
  pageSize: number = 10;
  pageLength: number = 0;

  photoSearchByMapping: Record<(string | PhotoSearchBy), string> = PhotoSearchByMapping;
  photoSearchBys: (string | PhotoSearchBy)[] = Object.values(PhotoSearchBy)
    .filter(value => typeof value === 'number');

  get f() {
    return this.formSearch.controls;
  }

  constructor(private _photoService: PhotoService,
    private _formBuilder: FormBuilder,
    private _matSnackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.buildForm();

    this.f['searchBy'].valueChanges.subscribe(val => {
      if (val === PhotoSearchBy.All) {
        this.f['search'].clearValidators();
        this.f['search'].disable();
      } else {
        this.f['search'].setValidators([Validators.required]);
        this.f['search'].enable();
      }

      this.f['search'].updateValueAndValidity();

      this.photos = [];
    });
  }

  onSubmit(): void {
    let paginationFilter: PaginationFilter = {
      PageNumber: 1,
      PageSize: this.pageSize
    };

    this.isLoading = true;

    if (!this.formSearch.valid) {
      this.isLoading = false;
      return;
    }

    this.enableControls(false);

    let search = this.f['search'].value;
    let searchBy = this.f['searchBy'].value;

    this._photoService
      .get(search, searchBy, paginationFilter)
      .subscribe({
        next: (photosResponse: PagedResponse<Photo[]>) => this.handleSearchResponse(photosResponse),
        error: (err: Error) => {
          this._matSnackBar.open(err.message, 'Ok');
          this.isLoading = false;
          this.enableControls(true);
        }
      });
  }

  onPage(event: PageEvent): void {
    let paginationFilter: PaginationFilter = {
      PageNumber: event.pageIndex + 1,
      PageSize: this.pageSize
    };

    this.searchPhotos(paginationFilter);
  }

  searchPhotos(paginationFilter: PaginationFilter): void {
    this.isLoadingPhotos = true;

    let search = this.f['search'].value;
    let searchBy = this.f['searchBy'].value;

    this._photoService
      .get(search, searchBy, paginationFilter)
      .subscribe({
        next: (photosResponse: PagedResponse<Photo[]>) => {
          this.isLoadingPhotos = false;

          if (photosResponse) {
            this.photos = photosResponse.Data;
            this.pageLength = photosResponse.TotalRecords;
          }
        },
        error: (err: Error) => {
          this._matSnackBar.open(err.message, 'Ok');
          this.isLoadingPhotos = false;
        }
      });
  }

  private handleSearchResponse(photosResponse: PagedResponse<Photo[]>): void {
    console.log(photosResponse);
    this.isLoading = false;
    this.enableControls(true);

    if (photosResponse) {
      this.photos = photosResponse.Data;
      this.pageLength = photosResponse.TotalRecords;
    }
  }

  private buildForm(): void {
    this.formSearch = this._formBuilder.group({
      search: [{value: '', disabled: true }],
      searchBy: [PhotoSearchBy.All, Validators.required],
    });
  }

  private enableControls(enable: boolean): void {
    if (enable) {
      this.f['search'].enable();
      this.f['searchBy'].enable();
      return;
    }

    this.f['search'].disable();
    this.f['searchBy'].disable();
  }
}
