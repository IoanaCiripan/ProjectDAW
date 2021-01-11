import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

@Injectable()
export class MovieService {
  constructor(private http: HttpClient) { }
  baseUrl: string = '/api/Movies';

  getMovies() {
    return this.http.get<any>(this.baseUrl)
  }

  getMovie(id) {
    return this.http.get<any>(this.baseUrl + "/" + id);
  }
}

