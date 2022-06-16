import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@core/service/auth.service';

interface Navigation {
  icon: string,
  text: string,
  url: string
}

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  navigations: Navigation[] = [];
  currentRoute: string = '/home';

  constructor(public router: Router, private _authService: AuthService) { }

  ngOnInit(): void {
    this.navigations = [
      {
        icon: 'travel_explore',
        text: 'Search images online',
        url: '/home'
      },
      {
        icon: 'saved_search',
        text: 'Browse saved images',
        url: '/saved-photos'
      },
      {
        icon: 'list',
        text: 'My list',
        url: '/my-list'
      }
    ];
  }

  logout(): void {
    this._authService.logout();
  }
}
