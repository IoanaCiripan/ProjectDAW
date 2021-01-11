import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

@Injectable()
export class ProfileService {
  constructor(private http: HttpClient) { }
  baseUrl: string = '/api/Profiles';

  getCurrentProfile(profileId) {
    return this.http.get<any>(this.baseUrl + "/" + profileId);
  }

  updateCurrentProfile(profile) {
    return this.http.put(this.baseUrl + "/" + profile.profileId, profile);
  }

  createProfile(profile) {
    return this.http.post(this.baseUrl, profile);
  }
}

