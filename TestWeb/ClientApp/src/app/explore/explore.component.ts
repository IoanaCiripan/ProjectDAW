import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { MovieService } from '../services/movie.service';

@Component({
  selector: 'app-explore',
  templateUrl: './explore.component.html',
  styleUrls: ['./explore.component.scss'],
  providers: [MovieService],
})
export class ExploreComponent implements OnInit {

  //variabiles
  movies: any[] = [];
  textSearch: boolean = true;

  //initialisations
  constructor(private movieService: MovieService) {
    this.movieService.getMovies().subscribe(res => {
      this.movies = res;
    }, err => {
      alert("Eroare");
    });
  }

  ngOnInit() {

  }

}
