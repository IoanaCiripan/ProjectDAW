import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from './admin-layout.routing';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { ExploreComponent } from '../../explore/explore.component';
import { CollectionComponent } from '../../collection/collection.component';
import { LogoutComponent } from '../../logout/logout.component';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import {
  MatButtonModule,
  MatInputModule,
  MatRippleModule,
  MatFormFieldModule,
  MatTooltipModule,
  MatSelectModule,
  MatAutocompleteModule,
  MatTabsModule,
  MatDividerModule,
  MatCardModule,
  MatCheckboxModule,
  MatProgressBarModule
} from '@angular/material';
import { MovieService } from '../../services/movie.service';
import { ProfileService } from '../../services/profile.service';
import { MovieCollectionService } from '../../services/movie-collection.service';
import { MovieDetailsComponent } from '../../movie-details/movie-details.component';
import { CommentService } from '../../services/comment.service';
import { AdminPanelComponent } from '../../admin-panel/admin-panel.component';


@NgModule({
  imports: [
    MatTabsModule,
    MatAutocompleteModule,
    MatDividerModule,
    MatCardModule,
    MatCheckboxModule,
    MatProgressBarModule
  ],
  exports: [
    MatTabsModule,
    MatDividerModule,
    MatAutocompleteModule,
    MatCardModule,
    MatCheckboxModule,
    MatProgressBarModule
  ],
})
export class AngularMaterial { }

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    MatButtonModule,
    MatRippleModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatTooltipModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatTabsModule,
    MatDividerModule,
    MatCardModule,
    MatCheckboxModule,
    MatProgressBarModule,
    Ng2SmartTableModule
  ],
  declarations: [
    UserProfileComponent,
    ExploreComponent,
    CollectionComponent,
    LogoutComponent,
    MovieDetailsComponent,
    AdminPanelComponent,
  ],
  providers: [
    MovieService,
    ProfileService,
    MovieCollectionService,
    CommentService,
  ]
})

export class AdminLayoutModule { }
