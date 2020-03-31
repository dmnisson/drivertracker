import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-about-page',
  templateUrl: './about-page.component.html',
  styleUrls: ['./about-page.component.sass']
})
export class AboutPageComponent implements OnInit {

  title = "About DriverTracker";

  message = "DriverTracker is a system for analyzing the performance of "
    + "drivers for taxicabs and transportation network carriers.";

  constructor(
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
  }

}
