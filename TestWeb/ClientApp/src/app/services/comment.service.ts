import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

@Injectable()
export class CommentService {
  constructor(private http: HttpClient) { }
  baseUrl: string = '/api/Comments';

  getComments() {
    return this.http.get<any>(this.baseUrl)
  }

  createComment(comment) {
    return this.http.post(this.baseUrl, comment);
  }
}

