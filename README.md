# drivertracker
An application for taxicab companies and transportation network carriers to track and analyze wait times, on-time performance, and service quality of drivers. 

Warning: This application is still in its early stages of development. Security of personal information is not guaranteed.

Specific features that are planned to be implemented are detailed on the About page. 

This is a work in progress. When core functionality has been developed, this README will be updated accordingly.

## Current features

Currently, DriverTracker features a means to add legs of each driver manually and analyze basic statistics for all drivers in the database as well as for an individual driver.
DriverTracker also includes a means to predict the probability of multiple ridership from data on a given driver.

## Planned features

* DriverTracker will use geocoding, k-means clustering, and logistic regression to analyze for different geographic regions the probability of collecting a fare given leg length and destination cluster.
* DriverTracker will include a mobile iOS and Android app that allows companies to collect leg data automatically.

## Technical details
* Server side written in C# using ASP.NET Core 2.1 MVC and Entity frameworks
* Client side written in TypeScript using Angular 7
* Built using Visual Studio 2017 for Mac and gulp and angular command-line tools
