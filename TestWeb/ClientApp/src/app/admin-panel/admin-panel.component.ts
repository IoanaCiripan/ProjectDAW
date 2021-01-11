import { Component, OnInit } from '@angular/core';
import { MovieService } from '../services/movie.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.scss'],
  providers: [MovieService]
})
export class AdminPanelComponent implements OnInit {

  isAdmin = false;

  constructor(
    private movieService: MovieService) {


    var user = JSON.parse(localStorage.getItem('currentUser'));
    if (user.user.userName == "admin") {
      this.isAdmin = true;
      this.fetchMovies();
    }
  }

  ngOnInit() {
  }

  fetchMovies() {
    this.movieService.getMovies().subscribe(res => {
      this.dataMovies = res;
    },
      err => {

      });
  }

  dataMovies = [];
  settingsMovies = {
    columns: {
      movieId: {
        title: 'ID'
      },
      title: {
        title: 'Movie Title'
      },
      duration: {
        title: 'Duration'
      },
      description: {
        title: 'Description'
      },
      trailerLink: {
        title: 'TrailerLink'
      },
    }
  };

}
