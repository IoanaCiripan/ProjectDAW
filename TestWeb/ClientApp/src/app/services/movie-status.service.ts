import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

@Injectable()
export class MovieStatusService {
  constructor(private http: HttpClient) { }
  baseUrl: string = '/api/MovieStatus';

  getMovieStatus() {
    return this.http.get<any>(this.baseUrl)
  }
}

