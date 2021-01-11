import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MovieService } from '../services/movie.service';
import { CommentService } from '../services/comment.service';
import { ProfileService } from '../services/profile.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MovieCollectionService } from '../services/movie-collection.service';

@Component({
  selector: 'app-movie-details',
  templateUrl: './movie-details.component.html',
  styleUrls: ['./movie-details.component.scss'],
  providers: [MovieService, CommentService, ProfileService, MovieCollectionService]
})
export class MovieDetailsComponent implements OnInit {

  currentMovie: any;
  comments: any[] = [];
  currentProfile: any;
  commentForm: FormGroup;

  constructor(public movieService: MovieService,
    private route: ActivatedRoute,
    private commentService: CommentService,
    private profileService: ProfileService,
    private movieCollectionService: MovieCollectionService,
    private fb: FormBuilder,
  ) {
    this.fetchMovie();
    this.fetchComments();
    this.fetchProfile();
    this.commentForm = this.fb.group({
      content: ['', Validators.required],
      upVoteNumber: [0, Validators.required],
      downVoteNumber: [0, Validators.required],
    });
  }

  ngOnInit() {

  }

  contemptLevel: number = 0;
  angerLevel: number = 0;
  disgustLevel: number = 0;
  happinessLevel: number = 0;
  sadnessLevel: number = 0;
  fearLevel: number = 0;
  surpriseLevel: number = 0;

  addToWishCollecion() {
      var wishCollectionMovie = {
          "movieId": this.currentMovie.movieId,
          "profileId": this.currentProfile.profileId,
          "movieStatusId": 2,
      };
      this.movieCollectionService.addMovieCollection(wishCollectionMovie).subscribe(res => {
          console.log("Succes!");
          console.log(wishCollectionMovie);
      },
      err => {
          console.log(err);
      });
  }

  addToWatchedCollecion() {
      var WatchedCollectionMovie = {
          "movieId": this.currentMovie.movieId,
          "profileId": this.currentProfile.profileId,
          "movieStatusId": 1,
      };
      this.movieCollectionService.addMovieCollection(WatchedCollectionMovie).subscribe(res => {
          console.log("Succes!");
          console.log(WatchedCollectionMovie);
      },
          err => {
              console.log(err);
          });
  }

  submitComment() {
    var user = JSON.parse(localStorage.getItem('currentUser'));
    this.profileService.getCurrentProfile(user.user.id).subscribe(succ => {
      //get current user
      this.currentProfile = succ;
      console.log(this.currentProfile);
      const movieId = this.route.snapshot.paramMap.get('id');
      this.movieService.getMovie(movieId).subscribe(res => {
        //get current movie
        this.currentMovie = res;
        console.log(this.currentMovie);
        var comment = {
          "content": this.commentForm.value.content,
          "upVoteNumber": this.commentForm.value.upVoteNumber,
          "downVoteNumber": this.commentForm.value.downVoteNumber,
          "movieId": this.currentMovie.movieId,
          "profileId": this.currentProfile.profileId,
        };
        this.commentService.createComment(comment).subscribe(
          res => {
            this.fetchComments();
          },
          err => {

          });
      }, err => {

      });
    }, err => {
    });
  }

  fetchProfile() {
    var user = JSON.parse(localStorage.getItem('currentUser'));
    this.profileService.getCurrentProfile(user.user.id).subscribe(succ => {
      //get current user
      this.currentProfile = succ;
      console.log(this.currentProfile);
      const movieId = this.route.snapshot.paramMap.get('id');
    }, err => {
    });
  }

  fetchComments() {
    const movieId = this.route.snapshot.paramMap.get('id');
    this.comments = [];
    this.commentService.getComments().subscribe(
      res => {
        res.forEach(c => {
          if (c.movieId == movieId)
            this.comments.push(c);
        });
        console.log(this.comments);
      },
      err => {

      }
    )
  }

  fetchMovie() {
    const movieId = this.route.snapshot.paramMap.get('id');
    this.movieService.getMovie(movieId).subscribe(res => {
      this.currentMovie = res;
      console.log(this.currentMovie);
    },
      err => {

      });
  }
}
