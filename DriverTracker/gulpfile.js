/// <binding Clean='clean' />
"use strict";

const gulp = require("gulp"),
      rimraf = require("rimraf"),
      concat = require("gulp-concat"),
      cssmin = require("gulp-cssmin"),
      uglify = require("gulp-uglify"),
      merge = require("merge-stream");

const paths = {
  webroot: "./wwwroot/",
  webroot_vendor: "./wwwroot/lib/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

gulp.task("clean:js", done => rimraf(paths.concatJsDest, done));
gulp.task("clean:css", done => rimraf(paths.concatCssDest, done));
gulp.task("clean:vendor", done => rimraf(paths.webroot_vendor, done));
gulp.task("clean", gulp.series(["clean:js", "clean:css"]));

gulp.task("min:js", () => {
  var streams = [gulp.src([paths.js, "!" + paths.minJs], { base: "." })
    .pipe(concat(paths.concatJsDest))
    .pipe(uglify())
    .pipe(gulp.dest("."))];

  return merge(streams);
});

gulp.task("min:css", () => {
  return gulp.src([paths.css, "!" + paths.minCss])
    .pipe(concat(paths.concatCssDest))
    .pipe(cssmin())
    .pipe(gulp.dest("."));
});

gulp.task("min", gulp.series(["min:js", "min:css"]));

// Dependency Dirs
var deps = {
  "@angular/common" : {"bundles/*" : ""},
  "@angular/core" : {"bundles/*" : ""},
  "@angular/compiler" : {"bundles/*" : ""},
  "@angular/platform-browser" : {"bundles/*" : ""},
  "@angular/platform-browser-dynamic" : {"bundles/*" : ""},
  "@angular/http" : {"bundles/*" : ""},
  "@angular/router" : {"bundles/*" : ""},
  "@angular/forms" : {"bundles/*" : ""},

  "rxjs" : {"bundles/*" : ""},
  "zone.js" : {"dist/*" : ""}
}

gulp.task("scripts", () => {
  var streams = [];

  for (var prop in deps) {
    console.log("Preparing scripts for: " + prop);
    for (var itemProp in deps[prop]) {
      streams.push(gulp.src("node_modules/" + prop + "/" + itemProp)
        .pipe(gulp.dest(paths.webroot_vendor + prop + "/" + deps[prop][itemProp])));
    }
  }

  return merge(streams);
});