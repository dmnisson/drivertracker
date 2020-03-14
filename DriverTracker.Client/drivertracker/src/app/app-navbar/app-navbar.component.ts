import { Component, OnInit } from '@angular/core';
import { faSignInAlt, faSignOutAlt } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-navbar',
  templateUrl: './app-navbar.component.html',
  styleUrls: ['./app-navbar.component.sass']
})
export class AppNavbarComponent implements OnInit {

  token: String;
  faSignInAlt = faSignInAlt;
  faSignOutAlt = faSignOutAlt;

  constructor() { }

  ngOnInit() {
  }

  logout() {
  }

}
