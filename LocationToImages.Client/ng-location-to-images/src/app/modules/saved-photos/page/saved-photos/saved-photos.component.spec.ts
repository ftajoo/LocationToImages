import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SavedPhotosComponent } from './saved-photos.component';

describe('SavedPhotosComponent', () => {
  let component: SavedPhotosComponent;
  let fixture: ComponentFixture<SavedPhotosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SavedPhotosComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SavedPhotosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
