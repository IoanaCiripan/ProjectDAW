import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../services/profile.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css'],
  providers: [ProfileService]
})
export class UserProfileComponent implements OnInit {

  disableEditMode: boolean = true;
  disableEmailInput: boolean = true;
  currentProfile: any;
  profileForm: FormGroup;
  checkEmtoions: boolean[] = [false, false, false, false, false, false, false];

  constructor(private profileService: ProfileService,
    private fb: FormBuilder,
  ) {
    this.profileForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      description: [''],
      username: [''],
    });
    var user = JSON.parse(localStorage.getItem('currentUser'));
    var isNewUser = localStorage.getItem('newUser');
    if (isNewUser == "true") {
      //create new profile
      var profile = {
        "firstName": "",
        "lastName": "",
        "description": "",
        "photo": "",
        "userId": user.user.id,
      };
      this.profileService.createProfile(profile).subscribe(res => {
        this.profileService.getCurrentProfile(user.user.id).subscribe(succ => {
        this.currentProfile = succ;
          console.log(this.currentProfile);
          this.profileForm = this.fb.group({
            firstName: [this.currentProfile.firstName, Validators.required],
            lastName: [this.currentProfile.lastName, Validators.required],
            description: [this.currentProfile.description],
          });
          this.profileForm.disable();
          console.log(this.currentProfile);
        }, err => {
          alert("eroare");
        });
      }, err => { })
    } else {
      this.profileService.getCurrentProfile(user.user.id).subscribe(succ => {
        this.currentProfile = succ;
        this.profileForm = this.fb.group({
          firstName: [this.currentProfile.firstName, Validators.required],
          lastName: [this.currentProfile.lastName, Validators.required],
          description: [this.currentProfile.description],
        });
        this.profileForm.disable();
        console.log(this.currentProfile);
      }, err => {
        alert("eroare");
      });
    }
  }

  switchMode() {
    this.profileForm.enable();
    this.disableEditMode = false;
  }

  onSubmit() {
    this.disableEditMode = true;
    this.profileForm.disable();
    this.currentProfile.firstName = this.profileForm.value.firstName;
    this.currentProfile.lastName = this.profileForm.value.lastName;
    this.currentProfile.description = this.profileForm.value.description;
    this.profileService.updateCurrentProfile(this.currentProfile).subscribe(succ => {
      console.log("Profile updated with success!");
    }, err => {
      console.warn("Error! Profile not updated!");
    });
  }

  ngOnInit() {
  }

}
