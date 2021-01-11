import { Routes } from '@angular/router';
import { UserProfileComponent } from '../../user-profile/user-profile.component';
import { ExploreComponent } from '../../explore/explore.component';
import { CollectionComponent } from '../../collection/collection.component';
import { LogoutComponent } from '../../logout/logout.component';
import { MovieDetailsComponent } from '../../movie-details/movie-details.component';
import { AdminPanelComponent } from '../../admin-panel/admin-panel.component';

export const AdminLayoutRoutes: Routes = [
  { path: 'user-profile', component: UserProfileComponent },
  { path: 'explore', component: ExploreComponent },
  { path: 'collection', component: CollectionComponent },
  { path: 'movie-details/:id', component: MovieDetailsComponent},
  { path: 'logout', component: LogoutComponent },
  { path: 'admin-panel', component: AdminPanelComponent }
];
