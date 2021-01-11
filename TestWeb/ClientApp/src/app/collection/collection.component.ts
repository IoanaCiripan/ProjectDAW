import { Component, OnInit } from '@angular/core';
import { MovieCollectionService } from '../services/movie-collection.service';

@Component({
  selector: 'app-collection',
  templateUrl: './collection.component.html',
  styleUrls: ['./collection.component.scss'],
  providers: [MovieCollectionService]
})
export class CollectionComponent implements OnInit {

  // variabiles
  selectedStatus: string = "all";
  statusesList: any[] = ["all", "watched", "wish"];
  collectionCardsList: any;

  //initialisations
  constructor(private movieCollectionService: MovieCollectionService) {
    this.fetchCollection();
  }

  ngOnInit() {

  }

  fetchCollection() {
    this.movieCollectionService.getMovieCollections().subscribe(succ => {
      this.collectionCardsList = succ;
      console.log(this.collectionCardsList);
    }, err => {

    });
  }

  changeStatus(event) {
    if (event.isUserInput == true) {
      this.selectedStatus = event.source.value;
    }
  }
}
