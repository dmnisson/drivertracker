﻿/// <binding Clean='clean' />
"use strict";

const gulp = require("gulp"),
      rimraf = require("rimraf"),
      concat = require("gulp-concat"),
      cssmin = require("gulp-cssmin"),
      ts = require("gulp-typescript"),
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
paths.tsDest = paths.webroot + "app/**/*.js";

gulp.task("clean:js", done => rimraf(paths.concatJsDest, done));
gulp.task("clean:css", done => rimraf(paths.concatCssDest, done));
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

}

// TypeScript apths
var tsPaths = paths.webroot + "app/*.ts"
var libTsPaths = paths.webroot + "lib/**/*.ts"

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

gulp.task("ts", () => {
    var sourcePaths = [tsPaths, libTsPaths];

    return gulp.src(sourcePaths, {base: '.'}).pipe(tsProject())
       .js.pipe(gulp.dest('.'));
});

gulp.task("default", gulp.series(["clean", "min", "scripts"]));