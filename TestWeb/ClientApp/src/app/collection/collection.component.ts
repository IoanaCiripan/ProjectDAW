import { Component, OnInit } from '@angular/core';
import { MovieCollectionService } from '../services/movie-collection.service';
import { ProfileService } from '../services/profile.service';

@Component({
  selector: 'app-collection',
  templateUrl: './collection.component.html',
  styleUrls: ['./collection.component.scss'],
  providers: [MovieCollectionService]
})
export class CollectionComponent implements OnInit {

    // variabiles
    selectedStatus: string = "all";
    currentProfile: any;
    statusesList: any[] = ["all", "watched", "wish"];
    collectionCardsList: any;

    //initialisations
    constructor(private movieCollectionService: MovieCollectionService,
        private profileService: ProfileService) {
        this.fetchCollection();
    }

    ngOnInit() {

    }

    fetchCollection() {
        var user = JSON.parse(localStorage.getItem('currentUser'));
        this.profileService.getCurrentProfile(user.user.id).subscribe(succ => {
            this.currentProfile = succ;
            console.log(this.currentProfile);
            this.movieCollectionService.getMovieCollections().subscribe(succ => {
                this.collectionCardsList = succ.filter(f => f.profileId == this.currentProfile.profileId);
            }, err => {

            });
        }, err => {
            alert("eroare");
        });
    }

    changeStatus(event) {
        if (event.isUserInput == true) {
            this.selectedStatus = event.source.value;
        }
    }
}
