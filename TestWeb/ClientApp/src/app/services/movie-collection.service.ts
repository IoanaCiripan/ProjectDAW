import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

@Injectable()
export class MovieCollectionService {
  constructor(private http: HttpClient) { }
  baseUrl: string = '/api/MovieCollections';

  getMovieCollections() {
    return this.http.get<any>(this.baseUrl)
  }

    addMovieCollection(movieCollection) {
        console.log("***");
    console.log(movieCollection);
    return this.http.post(this.baseUrl, movieCollection);
  }
}

